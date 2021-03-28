using PersonalLanguageDictionaryUI.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalLanguageDictionaryUI.Application.Interfaces
{
    public interface IPersonalLanguageDictionaryService
    {
        Task<List<SimpleTranslation>> GetOneRandom();
        Task<SimpleTranslation> GetIndexedTranslation(int index);
        Task<bool> ValidateProvidedTranslation(ProvidedSimpleTranslationAnswer providedSimpleTranslationAnswer);
    }
}