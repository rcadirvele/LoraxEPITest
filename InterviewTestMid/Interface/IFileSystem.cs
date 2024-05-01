using System;
namespace InterviewTestMid.Interface
{
	public interface IFileSystem
	{
        void AppendAllLines(string path, IEnumerable<string> lines);

        void AppendAllText(string path, string appendString);

        string ReadAllText(string path);

    }
}

