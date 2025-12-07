using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TechnicalInterview.Core.Application.Services.Transfers.Commands;
using TechnicalInterview.WebAPI.Dtos.Request;
using TechnicalInterview.WebAPI.Dtos.Response;

namespace TechnicalInterview.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TransfersController : BaseApiController
    {
        public TransfersController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> ExecuteTransfer([FromBody] TransferRequest request)
        {
            try
            {
                var command = new ExecuteTransferCommand(request.FromAccountId, request.ToAccountId, request.Amount, request.Description);
                var result = await Mediator.Send(command);
                if (!result.Succeeded)
                    return BadRequest(result);


                return Ok(result);
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
