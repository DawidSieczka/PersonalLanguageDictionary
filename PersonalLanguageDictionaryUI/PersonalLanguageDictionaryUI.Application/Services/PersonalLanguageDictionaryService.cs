using Newtonsoft.Json;
using PersonalLanguageDictionaryUI.Application.Interfaces;
using PersonalLanguageDictionaryUI.Application.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLanguageDictionaryUI.Application.Services
{
    public class PersonalLanguageDictionaryService : IPersonalLanguageDictionaryService
    {
        private readonly HttpClient _httpClient;

        public PersonalLanguageDictionaryService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44327/");
        }

        public async Task<List<SimpleTranslation>> GetOneRandom()
        {
            var response = await _httpClient.GetAsync("api/LanguageDictionary");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<SimpleTranslation>>(content);
            return result;
        }

        public async Task<SimpleTranslation> GetSingleRandomTranslation()
        {
            var response = await _httpClient.GetAsync("api/LanguageDictionary/single/random");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SimpleTranslation>(content);
            return result;
        }

        public async Task<SimpleTranslation> GetIndexedTranslation(int index)
        {
            var response = await _httpClient.GetAsync($"api/LanguageDictionary/single/{index}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SimpleTranslation>(content);
            return result;
        }

        public async Task<bool> ValidateProvidedTranslation(ProvidedSimpleTranslationAnswer providedSimpleTranslationAnswer)
        {
            Console.WriteLine("asdasddasdadasda");
            var json = JsonConvert.SerializeObject(providedSimpleTranslationAnswer);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/LanguageDictionary/single/validation", content);
            var result = bool.Parse(await response.Content.ReadAsStringAsync());

            return result;
        }
    }
}