using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnicalInterview.Core.Application.Dtos;
using TechnicalInterview.Core.Application.Services.Accounts.Queries;

namespace TechnicalInterview.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountsController: BaseApiController
    {

        public AccountsController(IMediator mediator) : base(mediator) { }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccount(string accountId)
        {
            var command = new GetAccountInfoQuery(accountId);
            var resp = await Mediator.Send(command);
            if (resp is null) return NotFound(resp);
            return Ok(resp);
        }

       /* [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> Deposit(string accountId, [FromBody] DepositDto request)
        {
            var command = new CreateDepositCommand(accountId, request.Amount);
            var resp = await Mediator.Send(command);
            if (!resp.Succeeded) return BadRequest(resp);
            return Ok(resp);
        }*/
    }
}
