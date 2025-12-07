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
        public async Task<IActionResult> GetAccount(string accountId)
        {
            try
            {
                var query = new GetAccountInfoQuery(accountId);
                var resp = await Mediator.Send(query);
                if (resp is null) return NotFound(ApiResponse<object>.Fail("No se encontro la cuenta"));
                return Ok(ApiResponse<AccountResponse>.Success(resp,"Cuenta consultada con exito"));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ApiResponse<object>.Fail(ex.Message));
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
                var command = new CreateDepositCommand(accountId, request.Amount, request.Description);
                var resp = await Mediator.Send(command);

                return Ok(ApiResponse<DepositResponse>.Success(resp,"Deposito realizado con exito"));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ApiResponse<object>.Fail(ex.Message));
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
                var resp = await Mediator.Send(command);
                return Ok(ApiResponse<WithdrawalResponse>.Success(resp,"Retiro realizado con exito"));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ApiResponse<object>.Fail(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.Fail($"Ocurrió un error al procesar la solicitud: {ex.Message}"));
            }
        }
    }
}
