using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnicalInterview.Core.Application.Dtos.Request;
using TechnicalInterview.Core.Application.Dtos.Response;
using TechnicalInterview.Core.Application.Services.Accounts.Commands;
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
            var query = new GetAccountInfoQuery(accountId);
            var resp = await Mediator.Send(query);
            if (resp is null) return NotFound(ApiResponse<object>.Fail("No se encontro la cuenta"));
            return Ok(ApiResponse<AccountResponseDto>.Success(resp));
        }

        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> CreateDeposit(string accountId, [FromBody] DepositRequestDto request)
        {
            var command = new CreateDepositCommand(accountId, request.Amount,request.Description);
            var resp = await Mediator.Send(command);
            if (resp.ErrorCode!=0) return BadRequest(ApiResponse<DepositResponseDto>.Fail(resp.ErrorMessage));
            return Ok(ApiResponse<object>.Success(resp));
        }
    }
}
