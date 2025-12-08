using AutoMapper;
using MediatR;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using TechnicalInterview.Core.Application.Dtos;
using TechnicalInterview.Core.Domain.Interfaces.Repositories;
using TechnicalInterview.Infrastructure.Repositories;
using TechnicalInterview.WebAPI.Dtos.Response;

namespace TechnicalInterview.Core.Application.Services.Transfers.Commands
{
    public class ExecuteTransferCommand : IRequest<ApiResponse<TransferResponse>>
    {
        public string FromAccountId { get; set; } = default!;
        public string ToAccountId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public ExecuteTransferCommand(string fromAccountId, string toAccountId, decimal amount, string description)
        {
            FromAccountId = fromAccountId;
            ToAccountId = toAccountId;
            Amount = amount;
            Description = description;
        }
    }

    public class ExecuteTransferHandler : IRequestHandler<ExecuteTransferCommand, ApiResponse<TransferResponse>>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IMapper _mapper;
        public ExecuteTransferHandler(ITransferRepository transferRepository, IMapper mapper)
        {
            _transferRepository = transferRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<TransferResponse>> Handle(ExecuteTransferCommand request, CancellationToken cancellationToken)
        {
            if (request.Amount <= 0)
            {
                return ApiResponse<TransferResponse>.Fail("El monto de la tranferencia debe ser mayor a cero", new List<string> { "El valor del campo amount debe ser mayor a cero" });
            }

            if (request.FromAccountId== request.ToAccountId)
            {
                return ApiResponse<TransferResponse>.Fail("La cuenta de origen y la cuenta destino no pueden ser la misma", new List<string> { "Los valores de los campo fromAccountId y toAccountId deben ser diferentes" });
            }
            var transfer = await _transferRepository.ExecuteTransfer(request.FromAccountId, request.ToAccountId, request.Amount, request.Description, cancellationToken);

            if (transfer is null)
            {
                return ApiResponse<TransferResponse>.Fail("No se logro recuperar informacion del deposito creado");
            }

            if (transfer.Success == false)
            {
                throw new ValidationException(transfer.Message);
            }

            var result= _mapper.Map<TransferResponse>(transfer);
            return ApiResponse<TransferResponse>.Success(result!, transfer.Message);
        }
    }
}
