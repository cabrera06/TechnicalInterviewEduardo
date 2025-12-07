using AutoMapper;
using TechnicalInterview.Core.Application.Dtos;
using TechnicalInterview.Core.Domain.Models;
using TechnicalInterview.WebAPI.Dtos.Response;
namespace TechnicalInterview.Core.Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountResponse>();
            CreateMap<Transaction, TransactionsList>();
            CreateMap<DepositDto, DepositResponse>();
            CreateMap<WithdrawalDto, WithdrawalResponse>();
        }
    }
}
