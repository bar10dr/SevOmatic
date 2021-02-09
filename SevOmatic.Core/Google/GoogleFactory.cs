using System;
using System.IO;
using System.Threading;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;

namespace SevOmatic.Core.Google
{
    public class GoogleFactory
    {
        SheetsService Service { get; set; }
        bool ShowErrors;
        public bool Running = false;
        string range = "Sheet1!A:S";
        string ApplicationName = "SevOmatic";
        string googleCredentialPath = "credentials.json";
        string googleTokenPath = "token.json";
        string[] Scopes = { SheetsService.Scope.Spreadsheets };

        public GoogleFactory(bool ShowErrors)
        {
            this.ShowErrors = ShowErrors;

            Connect();
        }

        //Establishes the thrust between Google and the app. It uses the credentials.json file from Google, and creates a token.json file which is used to communicate with Google
        void Connect()
        {
            try
            {
                using (var stream = new FileStream(googleCredentialPath, FileMode.Open, FileAccess.Read))
                {
                    var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(googleTokenPath, true)).Result;

                    Service = new SheetsService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = ApplicationName,
                    });
                }

                Running = true;
            }
            catch (Exception Ex)
            {
                ConsoleOutputHandler.WriteError(Ex, "An error occured while attempting to log into Google.", ShowErrors);
            }
        }

        //Creates a new spreadsheet on Google Spreadsheet
        public string CreateSpreadsheet(string SheetName)
        {
            try
            {
                Spreadsheet sheet = new Spreadsheet();
                sheet.Properties = new SpreadsheetProperties();
                sheet.Properties.Title = SheetName;

                return Service.Spreadsheets.Create(sheet).Execute().SpreadsheetId;
            }
            catch (Exception Ex)
            {
                ConsoleOutputHandler.WriteError(Ex, "An error occured while attempting to create a Google spreadsheet.", ShowErrors);
            }

            return string.Empty;
        }

        //Just in case we ever need to read values back from the spreadsheet for some reason, not in use
        //public IList<IList<object>> ReadFromSpreadsheet(string SpreadsheetId)
        //{
        //    try
        //    {
        //        return Service.Spreadsheets.Values.Get(SpreadsheetId, range).Execute().Values;
        //    }
        //    catch (Exception Ex)
        //    {
        //        ErrorHandler.WriteError(Ex, "An error occured while attempting to read Google Spreadsheet data", ShowErrors);
        //    }

        //    return null;
        //}

        //Updates a spreadsheet on Google Spreadsheets
        public void WriteToSpreadsheet(string SpreadsheetId, List<IList<object>> Data)
        {
            try
            {
                List<ValueRange> data = new List<ValueRange>();

                var valueRange = new ValueRange();
                valueRange.Range = range;

                valueRange.Values = Data;

                data.Add(valueRange);

                BatchUpdateValuesRequest requestBody = new BatchUpdateValuesRequest();
                requestBody.ValueInputOption = "RAW";
                requestBody.IncludeValuesInResponse = true;
                requestBody.Data = data;

                Service.Spreadsheets.Values.BatchUpdate(requestBody, SpreadsheetId).Execute();
            }
            catch (Exception Ex)
            {
                ConsoleOutputHandler.WriteError(Ex, "An error occured while attempting to write Google Spreadsheet data", ShowErrors);
            }
        }
    }
}
