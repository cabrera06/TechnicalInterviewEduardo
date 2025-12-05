using AutoMapper;
using MediatR;
using TechnicalInterview.Core.Application.Dtos;
using TechnicalInterview.Core.Domain.Interfaces.Repositories;

namespace TechnicalInterview.Core.Application.Services.Accounts.Queries
{
    public class GetAccountInfoQuery : IRequest<AccountDto>
    {
        public string AccountId { get; set; } = default!;

        public GetAccountInfoQuery(string accountId)
        {
            AccountId = accountId;
        }
    }


    public class GetAccountInfoHandler : IRequestHandler<GetAccountInfoQuery, AccountDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountInfoHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountDto> Handle(GetAccountInfoQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId, cancellationToken);
           // if (account == null) return null;

            var accountDto = _mapper.Map<AccountDto>(account);
            return accountDto;

        }
    }
}
