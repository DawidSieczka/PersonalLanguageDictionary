using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;

namespace Application.Services
{
    public class GoogleSpreadSheetService : IGoogleSpreadSheetService
    {
        public GoogleSpreadSheetService()
        {
        }

        private static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        private static string ApplicationName = "Google Sheets API .NET Quickstart";
        public SheetsService InitService()
        {
            UserCredential credential;
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }
        public async Task<List<string>> Get(string sheetName, string sheetID)
        {
            var service = InitService();
            
            // Define request parameters.
            String range = $"{sheetName}";//$"{sheetName}!A1:F10";
            var request = service.Spreadsheets.Values.Get(sheetID, range);
            var values = (await request.ExecuteAsync()).Values;
            var translation = new List<string>();
            
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    //row[0] - eng
                    //row[1] - pl (tłumaczenie);
                    translation.Add($"{row[0]} == {row[1]}");
                    
                    if(row[0] != "" && row[1] == "")
                    {
                        //translate

                    }
                }
            }

            return translation;
        }
    }
}