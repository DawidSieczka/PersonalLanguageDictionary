using Application.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
#if DEBUG
    [EnableCors("debugPolicy")]
#endif
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageDictionaryController : ApiController
    {
        private readonly IGoogleSpreadSheetService _googleSpreadSheetService;
        private readonly IProvidedSingleTranslationValidatorHandler _providedSingleTranslationValidatorHandler;
        private string _sheetName = "Arkusz1";//todo per user
        private string _sheetID = "1M0Isg49ajjc5LP9V3dJ-4cderroZ8YMkA5Xf7qsvRTw";//todo per user

        /// <summary>
        /// Basic constructor.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public LanguageDictionaryController(IMediator mediator, IGoogleSpreadSheetService googleSpreadSheetService, IProvidedSingleTranslationValidatorHandler providedSingleTranslationValidatorHandler) : base(mediator)
        {
            _googleSpreadSheetService = googleSpreadSheetService;
            _providedSingleTranslationValidatorHandler = providedSingleTranslationValidatorHandler;
        }

        [HttpGet]
        public async Task<List<SingleTranslation>> GetAnything()
        {
            return await _googleSpreadSheetService.GetAllTranslationsInSheet(_sheetName, _sheetID);
        }

        [HttpGet("single/random")]
        public async Task<SingleTranslation> GetSingleRandomTranslation()
        {
            return await _googleSpreadSheetService.GetSingleRandomTranslation(_sheetName, _sheetID);
        }

        [HttpGet("single/{index}")]
        public async Task<SingleTranslation> GetIndexedTranslation([FromRoute]int index)
        {
            return await _googleSpreadSheetService.GetIndexedTranslation(index, _sheetName, _sheetID);
        }

        [HttpPost("single/validation")]
        public async Task<bool> GetProvidedTranslationValidation([FromBody] ProvidedSingleTranslationAnswer providedSingleTranslationAnswer)
        {
            return await _providedSingleTranslationValidatorHandler.GetSimpleValidation(providedSingleTranslationAnswer);
        }

        
        [HttpPost]
        public async Task<string> CreateNewTranslation()
        {
            return await _googleSpreadSheetService.CreateNewRow(_sheetName,_sheetID);
        }
    }
}