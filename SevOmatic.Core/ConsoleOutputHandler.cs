using System;
using System.Collections.Generic;
using System.Text;

namespace SevOmatic.Core
{
    public static class ConsoleOutputHandler
    {
        public static void WriteMessage(string Message, bool RemoveFormatting = false)
        {
            switch (RemoveFormatting)
            {
                case false: Console.Write($"[{ DateTime.Now.ToString("HH:mm:ss") }] { Message }");
                    break;
                case true: Console.Write($"{ Message }");
                    break;
            };
        }

        public static void WriteLineMessage(string Message, bool RemoveFormatting = false)
        {
            switch (RemoveFormatting)
            {
                case false:
                    Console.WriteLine($"[{ DateTime.Now.ToString("HH:mm:ss") }] { Message }");
                    break;
                case true:
                    Console.WriteLine($"{ Message }");
                    break;
            };
        }

        public static void WriteError(Exception Ex, string Message, bool ShowErrors = false)
        {
            Console.WriteLine(Message);

            if (ShowErrors == true)
            {
                Console.WriteLine(Ex.Message);
                Console.WriteLine(Ex.StackTrace);
            }
        }
    }
}
