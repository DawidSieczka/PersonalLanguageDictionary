using System.Collections.Generic;
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
        public async Task<List<string>> GetAnything()
        {
            return await _googleSpreadSheetService.Get("Arkusz1", "1M0Isg49ajjc5LP9V3dJ-4cderroZ8YMkA5Xf7qsvRTw");
        }
    }
}