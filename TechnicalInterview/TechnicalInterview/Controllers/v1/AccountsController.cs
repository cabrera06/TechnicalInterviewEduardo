using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TechnicalInterview.Core.Application.Services.Accounts.Commands;
using TechnicalInterview.Core.Application.Services.Accounts.Queries;
using TechnicalInterview.WebAPI.Dtos;
using TechnicalInterview.WebAPI.Dtos.Request;
using TechnicalInterview.WebAPI.Dtos.Response;

namespace TechnicalInterview.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountsController : BaseApiController
    {

        public AccountsController(IMediator mediator) : base(mediator) { }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccount([FromRoute] AccountRequest accountRequest)
        {
            try
            {

                var query = new GetAccountInfoQuery(accountRequest.AccountId);
                var result = await Mediator.Send(query);
                if (!result.Succeeded)
                    return BadRequest(result);


                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.Fail($"Ocurrió un error al procesar la solicitud: {ex.Message}"));
            }
        }

        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> CreateDeposit([FromRoute] AccountRequest accountRequest, [FromBody] DepositRequest request)
        {
            try
            {
                var command = new CreateDepositCommand(accountRequest.AccountId, request.Amount, request.Description);
                var result = await Mediator.Send(command);

                if (!result.Succeeded)
                    return BadRequest(result);


                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.Fail($"Ocurrió un error al procesar la solicitud: {ex.Message}"));
            }
        }

        [HttpPost("{accountId}/withdrawal")]
        public async Task<IActionResult> CreateWithdrawal([FromRoute] AccountRequest accountRequest, [FromBody] WithdrawalRequest request)
        {
            try
            {
                var command = new CreateWithdrawalCommand(accountRequest.AccountId, request.Amount, request.Description);
                var result = await Mediator.Send(command);
                if (!result.Succeeded)
                    return BadRequest(result);


                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.Fail($"Ocurrió un error al procesar la solicitud: {ex.Message}"));
            }
        }
    }
}
