using System;
using System.IO;

namespace Stal.Shared.Log
{
    public class Logger : ILogger
    {
        public string FileName { get; }
        public bool IsWritingDate { get; }

        public Logger(string fileName, bool isWritingDate = true)
        {
            FileName = fileName;
            IsWritingDate = isWritingDate;
        }

        public void Log(string message)
        {
            if (IsWritingDate)
            {
                message = $"[{DateTime.Now}] {message}";
            }

            File.AppendAllText(FileName, message + '\n');
        }
    }
}
