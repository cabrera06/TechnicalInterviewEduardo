using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using TechnicalInterview.Core.Domain.Interfaces.Repositories;
using TechnicalInterview.WebAPI.Dtos.Response;

namespace TechnicalInterview.Core.Application.Services.Accounts.Commands
{
    public class CreateDepositCommand : IRequest<DepositResponse>
    {
        public string AccountId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string? Description { get; set; } 


        public CreateDepositCommand(string accountId, decimal amount, string description)
        {
            AccountId = accountId;
            Amount= amount;
            Description = description;
        }
    }

    public class CreateDepositHandler : IRequestHandler<CreateDepositCommand, DepositResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public CreateDepositHandler( IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task<DepositResponse> Handle(CreateDepositCommand request, CancellationToken cancellationToken)
        {

            if (request.Amount <= 0)
            {
                throw new ValidationException("El monto a depositar debe ser mayor a cero");
            }

            var deposit = await _accountRepository.CreateDeposit(request.AccountId, request.Amount, request.Description, cancellationToken);
            
            if (deposit is null)
            {
                throw new ValidationException("No se logro recuperar informacion del deposito creado");
            }

            if (deposit.Success ==false)
            {
                throw new ValidationException(deposit.Message);
            }


            return _mapper.Map<DepositResponse>(deposit);

        }
    }
}
