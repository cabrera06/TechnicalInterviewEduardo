using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using TechnicalInterview.Core.Domain.Interfaces.Repositories;
using TechnicalInterview.Infrastructure.Repositories;
using TechnicalInterview.WebAPI.Dtos.Response;

namespace TechnicalInterview.Core.Application.Services.Transfers.Commands
{
    public class ExecuteTransferCommand : IRequest<TransferResponse>
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

    public class ExecuteTransferHandler : IRequestHandler<ExecuteTransferCommand, TransferResponse>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IMapper _mapper;
        public ExecuteTransferHandler(ITransferRepository transferRepository, IMapper mapper)
        {
            _transferRepository = transferRepository;
            _mapper = mapper;
        }
        public async Task<TransferResponse> Handle(ExecuteTransferCommand request, CancellationToken cancellationToken)
        {
            if (request.Amount <= 0)
            {
                throw new ValidationException("El monto de la tranferencia debe ser mayor a cero");
            }

            if (request.FromAccountId== request.ToAccountId)
            {
                throw new ValidationException("La cuenta de origen y la cuenta destino no pueden ser la misma");
            }
            var transfer = await _transferRepository.ExecuteTransfer(request.FromAccountId, request.ToAccountId, request.Amount, request.Description, cancellationToken);

            if (transfer is null)
            {
                throw new ValidationException("No se logro recuperar informacion del deposito creado");
            }

            if (transfer.Success == false)
            {
                throw new ValidationException(transfer.Message);
            }


            return _mapper.Map<TransferResponse>(transfer);
        }
    }
}
