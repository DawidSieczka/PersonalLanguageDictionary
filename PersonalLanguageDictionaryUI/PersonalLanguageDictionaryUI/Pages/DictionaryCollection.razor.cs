using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PersonalLanguageDictionaryUI.Application.Interfaces;
using PersonalLanguageDictionaryUI.Application.Models;

namespace PersonalLanguageDictionaryUI.Pages
{
    public partial class DictionaryCollection : ComponentBase
    {
        [Inject] private IPersonalLanguageDictionaryService personalLanguageDictionaryService { get; set; }

        public List<SimpleTranslation> Words;

        private async Task OnClick_PopulateTable()
        {
            //test
            Words = await personalLanguageDictionaryService.GetOneRandom();
        }
    }
}