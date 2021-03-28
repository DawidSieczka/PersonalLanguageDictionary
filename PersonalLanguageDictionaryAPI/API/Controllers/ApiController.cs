using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Custom Base controller with the Mediator implementation.
    /// </summary>
    //[EnableCors]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        /// <summary>
        /// Mediator
        /// </summary>
        protected IMediator Mediator { get; }

        /// <summary>
        /// Basic constructor.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        protected ApiController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}