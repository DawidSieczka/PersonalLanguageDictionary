using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageDictionaryController : ApiController
    {
        private readonly IGoogleSpreadSheetService _googleSpreadSheetService;

        /// <summary>
        /// Basic constructor.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public LanguageDictionaryController(IMediator mediator, IGoogleSpreadSheetService googleSpreadSheetService) : base(mediator)
        {
            _googleSpreadSheetService = googleSpreadSheetService;
        }
        [HttpGet]
        public async Task<string> GetAnything()
        {
            return await _googleSpreadSheetService.Get();
        }
    }
}