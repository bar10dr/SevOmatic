using System;
using System.IO;
using Newtonsoft.Json;

namespace SevOmatic.Core.Settings
{
    //Used store and load app specific settings
    public static class SettingsFactory
    {
        public static string settingsFile = "settings.json";
        public static ApplicationSettings Settings = new ApplicationSettings();

        static SettingsFactory()
        {
            Load();
        }

        public static void Save()
        {
            try
            {
                File.WriteAllText(settingsFile, JsonConvert.SerializeObject(Settings));
            }
            catch(Exception Ex)
            {
                ConsoleOutputHandler.WriteError(Ex, "An error occured while trying to save the settings file.");
            }
        }

        static ApplicationSettings Load()
        {
            try
            {
                if (!File.Exists(settingsFile))
                {
                    Save();
                }

                Settings = JsonConvert.DeserializeObject<ApplicationSettings>(File.ReadAllText(settingsFile));

                return Settings;
            }
            catch (Exception Ex)
            {
                ConsoleOutputHandler.WriteError(Ex, "An error ocurred while trying to read the settings file.");
            }

            return new ApplicationSettings();
        }

        public static bool IsSpreadsheetIdEmpty => string.IsNullOrEmpty(Settings.SpreadsheetId);
    }
}
