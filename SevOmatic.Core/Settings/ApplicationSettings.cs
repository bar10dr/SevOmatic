using System;
using System.Collections.Generic;
using System.Text;

namespace SevOmatic.Core.Settings
{
    public class ApplicationSettings
    {
        //The ID of the Google spreadsheet to update
        public string SpreadsheetId { get; set; } = "";
        public bool Showlog { get; set; } = true;
        public bool ShowError { get; set; } = false;
    }
}
