
using System;
using System.Net.Http;
using System.Threading.Tasks;
using PersonalLanguageDictionaryUI.Application.Interfaces;

namespace PersonalLanguageDictionaryUI.Application.Services
{
    public class PersonalLanguageDictionaryService : IPersonalLanguageDictionaryService
    {
        private readonly HttpClient _httpClient;
        public PersonalLanguageDictionaryService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient();
            _httpClient.BaseAddress = new Uri("");
        }

        public async Task GetOneRandom()
        {
            throw new NotImplementedException();
        }
    }
}