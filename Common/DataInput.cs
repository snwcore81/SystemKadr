using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemKadr.Common
{
    public static class DataInput
    {
        public class DataInputException : Exception
        {
            public DataInputException(string? msg) : base(msg)
            {
            }
        }

        public static void ClearLine((int Left,int Top) position, ConsoleColor backgroundColor)
        {
            var cursorPos = Console.GetCursorPosition();
            var cursorVisible = Console.CursorVisible;
            var consoleBackgroundColor = Console.BackgroundColor;

            Console.BackgroundColor = backgroundColor;
            Console.CursorVisible = false;

            Console.SetCursorPosition(position.Left, position.Top);
            Console.Write(" ".PadRight(Console.WindowWidth));

            Console.BackgroundColor = consoleBackgroundColor;
            Console.CursorVisible = cursorVisible;
            Console.SetCursorPosition(cursorPos.Left, cursorPos.Top);
        }

        public static void Write((int Left, int Top) position, ConsoleColor foregroundColor, string text)
        {
            var cursorPos = Console.GetCursorPosition();
            var consoleForegroundColor = Console.ForegroundColor;

            Console.ForegroundColor = foregroundColor;
            Console.SetCursorPosition(position.Left, position.Top);

            Console.Write(text);

            Console.ForegroundColor = consoleForegroundColor;
            Console.SetCursorPosition(cursorPos.Left, cursorPos.Top);
        }

        public static T? Get<T>(string promptMsg, string errorMsg, Action<T?> action = null)
        {
            var cursorPos = Console.GetCursorPosition();

            T? result = default;

            do
            {
                ClearLine((cursorPos.Left, cursorPos.Top), Console.BackgroundColor);                

                Console.SetCursorPosition(cursorPos.Left, cursorPos.Top);

                Console.Write(promptMsg);

                try
                {
                    result = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));

                    action?.Invoke(result);

                    break;
                }
                catch (Exception e)
                {
                    string msg = errorMsg;

                    if (e is DataInputException || msg == null)
                        msg = e.Message;

                    ClearLine((cursorPos.Left, cursorPos.Top + 1), Console.BackgroundColor);
                    Write((cursorPos.Left, cursorPos.Top + 1), ConsoleColor.Red, msg);
                }
                
            }
            while (true);

            ClearLine((cursorPos.Left, cursorPos.Top + 1), Console.BackgroundColor);

            return result;
        }
    }
}
