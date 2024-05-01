using System;
using InterviewTestMid.Interface;
using System.Diagnostics;

namespace InterviewTestMid.Utility
{
    //Creating Wrapper as Debug class is called redundantly and will be ease for unit test. 
    internal class DebugWrapper : IDebugWrapper
    {
        public void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }
    }
}

