using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SystemKadr.Common.Interface;

namespace SystemKadr.Common.Logging
{
    public class Loger : IDisposable
    {
        private class DefaultLogWriter : ILogWriter
        {
            public void Write(string message) => Console.WriteLine(message);
        }

        private static ILogWriter? _logerWriter = new DefaultLogWriter();

        private static readonly string _fed = "|  ";
        private static int _globalLevel = 0;
        private string _typeName;
        private string _methodName;

        public enum Level
        {
            Info,
            Warning,
            Hint,
            Error
        }

        private Loger(string typeName, string methodName)
        {
            _typeName = typeName;
            _methodName = methodName + (methodName.Contains('C') && methodName.Contains(')') ? "" : "()");

            WriteLine(Level.Info, string.Empty);

            _globalLevel++;
        }

        private void WriteLine(Level lvl, string msg, params object[] args)
        {
            msg = $"{_typeName}::{_methodName}{msg}";

            string line = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff} {$"[{lvl}]",-10} {string.Concat(Enumerable.Repeat(_fed, _globalLevel))}{string.Format(msg, args)}";

            _logerWriter?.Write(line);
        }
        public void Dispose()
        {
            _globalLevel--;
        }

        public void Hint(string msg, params object[] args) => WriteLine(Level.Hint, $":{string.Format(msg, args)}");
        public void Info(string msg, params object[] args) => WriteLine(Level.Info, $":{string.Format(msg, args)}");
        public void Error(string msg, params object[] args) => WriteLine(Level.Error, $":{string.Format(msg, args)}");
        public void Warning(string msg, params object[] args) => WriteLine(Level.Warning, $":{string.Format(msg, args)}");

        public static void SetOutput(ILogWriter lw)
        {
            if (lw != null)
                _logerWriter = lw;
        }

        public static Loger Create<T>(string methodName) => new(typeof(T).Name.Replace("`1", "<T>"), methodName);
    }
}
