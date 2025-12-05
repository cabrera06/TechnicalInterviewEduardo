using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalInterview.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected IMediator Mediator { get; }

        protected BaseApiController(IMediator mediator)
        {

            Mediator = mediator;
        }
    }

}
