using System;
using System.Threading;
using System.Collections.Generic;
using CommandLine;
using SevOmatic.Core.Google;
using SevOmatic.Core.Settings;
using SevOmatic.Core.Trades;
using SevOmatic.Core;

namespace SevOmatic.Terminal
{
    class Program
    {
        static GoogleFactory googleFactory;
        static System.Timers.Timer timer = new System.Timers.Timer();

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<StartupOptions>(args)
                .WithParsed(Run)
                .WithNotParsed(HandleParseError);
        }

        static void Run(StartupOptions Options)
        {
            Init(Options);

            googleFactory = new GoogleFactory(Options.ShowError);

            if (googleFactory.Running == true)
            {
                //If we don't have the ID for a Google spreadsheet in the settings.json file; create a new Spreadsheet on Google and store its ID in settings.json
                if (SettingsFactory.IsSpreadsheetIdEmpty)
                {
                    ConsoleOutputHandler.WriteMessage($"SpreadsheetID not found; Attempting to create a new Spreadsheet named { Options.SpreadsheetName }... ");

                    SettingsFactory.Settings.SpreadsheetId = googleFactory.CreateSpreadsheet(Options.SpreadsheetName);
                    SettingsFactory.Save();

                    ConsoleOutputHandler.WriteLineMessage("Done!", true);
                }

                //Switch between run once or keep running until Q is pressed
                switch (Options.Mode)
                {
                    case RunMode.Single:
                        ConsoleOutputHandler.WriteLineMessage("Running single update...");

                        UpdateSpreadsheet();
                        break;

                    case RunMode.Continous:
                        bool exit = false;

                        ConsoleOutputHandler.WriteLineMessage("Running continous update... [Press Q to quit]");

                        timer.Elapsed += Timer_Elapsed;
                        timer.Interval = Options.UpdateFrequency * 1000;
                        timer.Start();

                        UpdateSpreadsheet();

                        while (exit == false)
                        {
                            if (Console.KeyAvailable)
                            {
                                var key = Console.ReadKey(true).Key;

                                if (key == ConsoleKey.Q || key.ToString().ToLower() == "q")
                                {
                                    exit = true;
                                }
                            }

                            Thread.Sleep(100);
                        }
                        break;

                        timer.Stop();
                }

                ConsoleOutputHandler.WriteLineMessage("Exiting...");
            }
        }

        static void Init(StartupOptions Options)
        {
            SettingsFactory.Settings.Showlog = Options.ShowLog;
            ConsoleOutputHandler.WriteLineMessage("SevOmatic v1.0.0", false);
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            UpdateSpreadsheet();
        }

        //Update the spreadsheet if the json file with the data changed since last update
        static void UpdateSpreadsheet()
        {
            var data = TradeFactory.ReadData();

            if (data.HasRefreshed)
            {
                googleFactory.WriteToSpreadsheet(SettingsFactory.Settings.SpreadsheetId, data.Data);
                ConsoleOutputHandler.WriteLineMessage("Spreadsheet updated");
            }
        }

        //Output error message if wrong attributes provided to the program
        static void HandleParseError(IEnumerable<Error> Errors)
        {
            foreach (var error in Errors)
            {
                Console.WriteLine($"Error: { error.Tag }");
            }
        }
    }
}
