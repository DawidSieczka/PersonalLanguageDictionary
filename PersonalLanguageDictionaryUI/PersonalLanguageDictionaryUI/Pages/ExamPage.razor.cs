using System;
using Microsoft.AspNetCore.Components;
using PersonalLanguageDictionaryUI.Application.Interfaces;
using PersonalLanguageDictionaryUI.Application.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace PersonalLanguageDictionaryUI.Pages
{
    public partial class ExamPage : ComponentBase
    {
        [Inject] private IPersonalLanguageDictionaryService _personalLanguageDictionaryService { get; set; }

        public bool IsTranslationLoaded;
        public SimpleTranslation Word;
        public int ExcelRecord = 1;
        public string InputValue;
        public bool IsAnswerValid = true;
        public bool IsAnswered = false;

        protected override async Task OnInitializedAsync()
        {
            await GetTranslation(++ExcelRecord);
        }

        private async Task GetTranslation(int index)
        {
            IsTranslationLoaded = false;

            Word = await _personalLanguageDictionaryService.GetIndexedTranslation(index);

            if (Word != null)
                IsTranslationLoaded = true;
        }

        public async Task GetNextTranslation()
        {
            await GetTranslation(++ExcelRecord);
            InputValue = string.Empty;
            IsAnswered = false;
        }

        public async Task SubmitEnteredValue()
        {
            if (IsAnswered)
            {
                await GetNextTranslation();
            }else if (InputValue != string.Empty)
            {
                await GetProvidedAnswerValidation();
            }
        }

        public async Task GetProvidedAnswerValidation()
        {
            IsAnswered = true;
            var providedSimpleTranslationAnswer = new ProvidedSimpleTranslationAnswer()
            {
                CorrectAnswer = Word.TranslatedWord,
                ProvidedAnswer = InputValue
            };

            IsAnswerValid = await _personalLanguageDictionaryService.ValidateProvidedTranslation(providedSimpleTranslationAnswer);
        }
        
    }
}