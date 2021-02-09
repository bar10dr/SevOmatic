﻿using SevOmatic.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SevOmatic.Core
{
    public static class ConsoleOutputHandler
    {
        public static void WriteMessage(string Message, bool RemoveFormatting = false)
        {
            if (SettingsFactory.Settings.Showlog == true)
            {
                switch (RemoveFormatting)
                {
                    case false: Console.Write($"[{ DateTime.Now.ToString("HH:mm:ss") }] { Message }");
                        break;
                    case true: Console.Write($"{ Message }");
                        break;
                };
            }
        }

        public static void WriteLineMessage(string Message, bool RemoveFormatting = false)
        {
            if (SettingsFactory.Settings.Showlog == true)
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
        }

        public static void WriteError(Exception Ex, string Message)
        {
            Console.WriteLine($"[ERROR] {Message}");

            if (SettingsFactory.Settings.ShowError == true)
            {
                Console.WriteLine(Ex.Message);
                Console.WriteLine(Ex.StackTrace);
            }
            else
            {
                Console.Write("Due to application settings, the specifics of the error was not shown. Start the application with -e to show error specifics.");
            }
        }
    }
}
