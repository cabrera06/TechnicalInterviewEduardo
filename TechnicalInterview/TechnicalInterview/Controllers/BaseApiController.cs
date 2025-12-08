using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalInterview.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        //Evito el service locator pattern
        //private IMediator _mediator;
        //protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        //Inyecto el mediator en el constructor ayuda a las pruebas unitarias
        protected IMediator Mediator { get; }

        protected BaseApiController(IMediator mediator)
        {

            Mediator = mediator;
        }
    }

}
