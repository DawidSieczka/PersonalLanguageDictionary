using Application.Interfaces;
using Application.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.AppendRequest;

namespace Application.Services
{
    public class GoogleSpreadSheetService : IGoogleSpreadSheetService
    {
        private readonly SheetsService service;

        public GoogleSpreadSheetService()
        {
            service = InitService();
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

        public async Task<List<SingleTranslation>> GetAllTranslationsInSheet(string sheetName, string sheetID)
        {
            var data = await GetDataFromSheet(sheetName, sheetID);
            var translation = new List<SingleTranslation>();

            if (data != null && data.Count > 0)
            {
                foreach (var row in data)
                {
                    var isTranslationALanguageHeader = data[0][0] == row[0];
                    if (isTranslationALanguageHeader)
                        continue;

                    var basicLanguage = data[0][0].ToString();
                    var translatedLanguage = data[0][1].ToString();
                    var basicWord = row[0].ToString();
                    var translatedWord = row[1].ToString();

                    translation.Add(new SingleTranslation() { BasicLanguage = basicLanguage, TranslatedLanguage = translatedLanguage, BasicWord = basicWord, TranslatedWord = translatedWord });
                }
            }

            return translation;
        }

        public async Task<SingleTranslation> GetSingleRandomTranslation(string sheetName, string sheetID)
        {
            var data = await GetDataFromSheet(sheetName, sheetID);
            var rand = new Random();
            var index = rand.Next(1, data.Count());
            var translation = data[index];

            var basicLanguage = data[0][0].ToString();
            var translatedLanguage = data[0][1].ToString();
            var basicWord = translation[0].ToString();
            var translatedWord = translation[1].ToString();

            return new SingleTranslation() { BasicLanguage = basicLanguage, TranslatedLanguage = translatedLanguage, BasicWord = basicWord, TranslatedWord = translatedWord };
        }

        public async Task<SingleTranslation> GetIndexedTranslation(int index, string sheetName, string sheetID)
        {
            var data = await GetDataFromSheetAsync(index, sheetName, sheetID);
            var translation = data[1];

            var basicLanguage = data[0][0].ToString();
            var translatedLanguage = data[0][1].ToString();

            var basicWord = translation[0].ToString();
            var translatedWord = translation[1].ToString();
            return new SingleTranslation() { BasicLanguage = basicLanguage, TranslatedLanguage = translatedLanguage, BasicWord = basicWord, TranslatedWord = translatedWord };
        }

        private async Task<IList<IList<object>>> GetDataFromSheet(string sheetName, string sheetID)
        {
            //var service = InitService();
            string range = $"{sheetName}"; //Define range == $"{sheetName}!A1:F10";
            var request = service.Spreadsheets.Values.Get(sheetID, range);
            var data = (await request.ExecuteAsync()).Values;
            return data;
        }

        private async Task<IList<IList<object>>> GetDataFromSheetAsync(int index, string sheetName, string sheetID)
        {
            var headerRange = $"{sheetName}!A1:B1";
            string range = $"{sheetName}!A{index}:B{index}";
            var headerRequest = service.Spreadsheets.Values.Get(sheetID, headerRange);
            var dataRequest = service.Spreadsheets.Values.Get(sheetID, range);

            var data = (await headerRequest.ExecuteAsync()).Values;
            var contentValues = (await dataRequest.ExecuteAsync()).Values;
            foreach (var item in contentValues)
            {
                data.Add(item);
            }

            return data;
        }

        public async Task<string> CreateNewRow(string sheetName,string sheetID)
        {
            var service = InitService();
            ValueRange requestBody = new ValueRange();
            string range = $"{sheetName}!A1:B1";
            SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(requestBody, sheetID, range);
            //interpered
            var valueInputOption = (ValueInputOptionEnum)0;  // TODO: Update placeholder value.
            // How the input data should be inserted.
            var insertDataOption = (InsertDataOptionEnum)0;  // TODO: Update placeholder value.

            request.ValueInputOption = valueInputOption;
            request.InsertDataOption = insertDataOption;
            AppendValuesResponse response = await request.ExecuteAsync();
            return JsonConvert.SerializeObject(response);


        }
    }
}