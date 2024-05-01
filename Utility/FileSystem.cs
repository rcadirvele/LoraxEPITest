using System;
using InterviewTestMid.Interface;

namespace InterviewTestMid.Utility
{

	public class FileSystem : IFileSystem
	{

        public void AppendAllLines(string path, IEnumerable<string> lines)
        {

            File.AppendAllLines(path, lines);

        }

        public void AppendAllText(string path, string appendString)
        {
         
            File.AppendAllText(path, appendString);
        }

        public string ReadAllText(string path)
        {
  

            return File.ReadAllText(path);
        }
    }
}

