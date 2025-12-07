using AutoMapper;
using Azure;
using MediatR;
using System.ComponentModel.DataAnnotations;
using TechnicalInterview.Core.Domain.Interfaces.Repositories;
using TechnicalInterview.WebAPI.Dtos.Response;

namespace TechnicalInterview.Core.Application.Services.Accounts.Queries
{
    public class GetAccountInfoQuery : IRequest<AccountResponse?>
    {
        public string AccountId { get; set; } = default!;

        public GetAccountInfoQuery(string accountId)
        {
            AccountId = accountId;
        }
    }


    public class GetAccountInfoHandler : IRequestHandler<GetAccountInfoQuery, AccountResponse?>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountInfoHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountResponse?> Handle(GetAccountInfoQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountInfo(request.AccountId, cancellationToken);
    
            if (account is null)
            {
                throw new ValidationException("La Cuenta indicada no existe");
            }
            
            var accountDto = _mapper.Map<AccountResponse?>(account);
            return accountDto;

        }
    }
}
