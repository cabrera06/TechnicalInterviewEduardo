using AutoMapper;
using Azure;
using MediatR;
using System.ComponentModel.DataAnnotations;
using TechnicalInterview.Core.Domain.Interfaces.Repositories;
using TechnicalInterview.WebAPI.Dtos.Response;

namespace TechnicalInterview.Core.Application.Services.Accounts.Queries
{
    public class GetAccountInfoQuery : IRequest<ApiResponse<AccountResponse>?>
    {
        public string AccountId { get; set; } = default!;

        public GetAccountInfoQuery(string accountId)
        {
            AccountId = accountId;
        }
    }


    public class GetAccountInfoHandler : IRequestHandler<GetAccountInfoQuery, ApiResponse<AccountResponse>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountInfoHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<AccountResponse>?> Handle(GetAccountInfoQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountInfo(request.AccountId, cancellationToken);
    
            if (account is null)
            {
                return ApiResponse<AccountResponse>.Fail("La Cuenta indicada no existe", new List<string> { "La cuenta "+ request.AccountId+" no existe" });
            }
            
            var accountDto = _mapper.Map<AccountResponse?>(account);
            return ApiResponse<AccountResponse>.Success(accountDto!, "Consulta Exitosa");

        }
    }
}
