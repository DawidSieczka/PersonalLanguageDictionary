using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;

namespace Application.Handlers
{
    public class ProvidedSingleTranslationValidatorHandler : IProvidedSingleTranslationValidatorHandler
    {
        public async Task<bool> GetSimpleValidation(ProvidedSingleTranslationAnswer providedSingleTranslationAnswer)
        {
            var correctAnswer = providedSingleTranslationAnswer.CorrectAnswer;
            var providedAnswer = providedSingleTranslationAnswer.ProvidedAnswer;

            if (correctAnswer == providedAnswer)
                return true;
            else
                return false;
        }
    }
}
