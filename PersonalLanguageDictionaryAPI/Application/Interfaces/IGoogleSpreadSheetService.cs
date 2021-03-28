using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Interfaces
{
    public interface IGoogleSpreadSheetService
    {
        Task<List<SingleTranslation>> GetAllTranslationsInSheet(string sheetName, string sheetID);
        Task<SingleTranslation> GetSingleRandomTranslation(string sheetName, string sheetID);
        Task<SingleTranslation> GetIndexedTranslation(int index, string sheetName, string sheetID);
        Task<string> CreateNewRow(string sheetName, string sheetID);
    }
}
