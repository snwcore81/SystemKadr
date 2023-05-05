using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemKadr.Common.Interface;

namespace SystemKadr.Common.Logging
{
    public class FileLogWriter : ILogWriter
    {
        private object _lock = new object();
        private string fileNameAndPath;

        public FileLogWriter(string fileNameAndPath)
        {
            this.fileNameAndPath = fileNameAndPath;
        }

        public void Write(string message)
        {
            lock (_lock)
            {
                File.AppendAllText(fileNameAndPath, message+ "\n");
            }
        }
    }
}
