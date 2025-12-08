using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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
        public async Task<IActionResult> GetAccount(string accountId)
        {
            try
            {

                var query = new GetAccountInfoQuery(accountId);
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
        public async Task<IActionResult> CreateDeposit(string accountId, [FromBody] DepositRequest request)
        {
            try
            {
                var command = new CreateDepositCommand( accountId, request.Amount, request.Description);
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
        public async Task<IActionResult> CreateWithdrawal(string accountId, [FromBody] WithdrawalRequest request)
        {
            try
            {
                var command = new CreateWithdrawalCommand(accountId, request.Amount, request.Description);
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
