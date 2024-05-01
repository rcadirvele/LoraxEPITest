    using System.Diagnostics;
    using System.IO;
    using InterviewTestMid.Interface;

    namespace InterviewTestMid
    {

        //------------Test Requirement--------------------------
        //1. Create an interface for the Logger class.
        //      -Update the logger class to implement the interface.
        //      -Add a new method to the logger which takes a list of strings & writes them as a CSV.

        //2. Create a new implementation of the logger class.
        //      -The LogMessage method should include a timestamp.
        //      -The other methods should not change.
        //------------Test Requirement--------------------------

        //---------Clarifications on Logger class --------------
        //   - Asumming that CSV log is only to log list of strings(i.e. material descriptions)
        //      and not writing the remaining log in any file(text or csv) for now, as it is not clear about the usage in req.
        //   - In req, Not mentioned about where to use new implementation of the logger class, so leaving it unused.
        //---------Clarifications on Logger class --------------

        public class Logger : ILogger
        {

            private readonly string _logFilePath;
            private readonly IDebugWrapper _debugWrapper;
            private readonly IFileSystem _fileSystem;

            public Logger(IDebugWrapper debugWrapper, IFileSystem fileSystem)
            {
                _debugWrapper = debugWrapper;
                _fileSystem = fileSystem;

                string logsDirectory = @"Logs";
                if (!Directory.Exists(logsDirectory))
                {
                    Directory.CreateDirectory(logsDirectory);
                }
                string csvFileName = $"Logs_{DateTime.Now:yyyyMMddHHmmssffff}.csv";

                //Created the csv file Logs folder in current Directory - bin/{debug||release}/net8.0/Logs/{logFileName}.
                _logFilePath = Path.Combine(logsDirectory, csvFileName);

            }

            public void WriteLogMessage(string LogMessage)
            {
                if (string.IsNullOrEmpty(LogMessage))
                    throw new ArgumentException("Log message not provided", nameof(LogMessage));

                _debugWrapper.WriteLine($"{LogMessage}");
            }

            public void WriteErrorMessage(Exception Ex)
            {
                if (Ex == null)
                    throw new ArgumentException("Exception not provided", nameof(Ex));

                _debugWrapper.WriteLine($"Error recieved: {Ex.Message}");
                _debugWrapper.WriteLine($"{Ex.StackTrace}");
            }

            public void WriteCSV(List<string> CSVMessages)
            {
                if (CSVMessages == null || CSVMessages.Count == 0)
                    throw new ArgumentException("List for CSV is null or empty", nameof(CSVMessages));

                _fileSystem.AppendAllLines(_logFilePath, CSVMessages);

            }
        }

   

        internal static class TimestampedLogger
        {    
            public static void WriteLogMessage(this ILogger logger, string LogMessage)
            {
                logger.WriteLogMessage($"DateTime.Now:yyyy-MM-dd HH:mm:ss - " + LogMessage);
            }

            public static void WriteErrorMessage(this ILogger logger, Exception Ex) => logger.WriteErrorMessage(Ex);

            public static void WriteCSV(this ILogger logger, List<string> CSVMessages) => logger.WriteCSV(CSVMessages);

        }
   
    }
