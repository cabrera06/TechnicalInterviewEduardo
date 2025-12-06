using AutoMapper;
using MediatR;
using TechnicalInterview.Core.Application.Dtos.Response;
using TechnicalInterview.Core.Domain.Interfaces.Repositories;

namespace TechnicalInterview.Core.Application.Services.Accounts.Commands
{
    public class CreateDepositCommand : IRequest<DepositResponseDto>
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

    public class CreateDepositHandler : IRequestHandler<CreateDepositCommand, DepositResponseDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public CreateDepositHandler( IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task<DepositResponseDto> Handle(CreateDepositCommand request, CancellationToken cancellationToken)
        {
            var deposit = await _accountRepository.CreateDeposit(request.AccountId, request.Amount, request.Description, cancellationToken);

            if (deposit is null)
            {
                return new DepositResponseDto
                {
                    ErrorCode = -1,
                    ErrorMessage= "No se logro btener informacion de deposito",
                };
             }
            return _mapper.Map<DepositResponseDto>(deposit);

        }
    }
}
