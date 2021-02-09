using CommandLine;

namespace SevOmatic.Terminal
{
    enum RunMode { Single, Continous }

    class StartupOptions
    {
        [Option('e', "showerror", Required = false, HelpText = "Shows full error in console. Deactivated as default because it might show Google keys.")]
        public bool ShowError { get; set; } = false;

        [Option('l', "showlog", Required = false, HelpText = "Shows the log.")]
        public bool ShowLog { get; set; } = true;

        [Option('r', "runmode", Required = false, HelpText = "[Single | Continous] Single = Will run once and quit. Continually = Will keep running until manually stopped.")]
        public RunMode Mode { get; set; } = RunMode.Continous;

        [Option('f', "updatefrequency", Required = false, HelpText = "Update frequency in seconds if in continous mode.")]
        public int UpdateFrequency { get; set; } = 5;

        [Option('n', "spreadsheetname", Required = false, HelpText = "Specify first time running to specify the name of the spreadsheet. Delete settings.json to start fresh.")]
        public string SpreadsheetName { get; set; } = "SevOmatic";
    }
}
