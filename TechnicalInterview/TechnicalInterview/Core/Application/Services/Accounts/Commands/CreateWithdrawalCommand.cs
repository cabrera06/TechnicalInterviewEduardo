using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using TechnicalInterview.Core.Domain.Interfaces.Repositories;
using TechnicalInterview.WebAPI.Dtos.Response;


namespace TechnicalInterview.Core.Application.Services.Accounts.Commands
{
    public class CreateWithdrawalCommand : IRequest<WithdrawalResponse>
    {
        public string AccountId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public CreateWithdrawalCommand(string accountId, decimal amount, string description)
        {
            AccountId = accountId;
            Amount = amount;
            Description = description;
        }

    }
    public class withdrawalHandler : IRequestHandler<CreateWithdrawalCommand, WithdrawalResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public withdrawalHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task<WithdrawalResponse> Handle(CreateWithdrawalCommand request, CancellationToken cancellationToken)
        {
            if (request.Amount <= 0)
            {
                throw new ValidationException("El monto a retirar debe ser mayor a cero");
            }
            var withdrawal = await _accountRepository.CreateWithdrawal(request.AccountId, request.Amount, request.Description, cancellationToken);
            if (withdrawal is null)
            {
                throw new ValidationException("No se logro recuperar informacion del retiro creado");
            }
            if (withdrawal.Success == false)
            {
                throw new ValidationException(withdrawal.Message);
            }

            return _mapper.Map<WithdrawalResponse>(withdrawal);

        }
    }

}
