namespace InterviewTestMid.Interface
{
    public interface ILogger
    {
        void WriteLogMessage(string message);

        void WriteErrorMessage(Exception exMessage);

        void WriteCSV(List<string> data);

    }

}

