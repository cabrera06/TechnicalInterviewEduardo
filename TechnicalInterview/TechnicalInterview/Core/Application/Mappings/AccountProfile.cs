using AutoMapper;
using TechnicalInterview.Core.Application.Dtos.Response;
using TechnicalInterview.Core.Domain.Models;
using TechnicalInterview.Infrastructure.SpResults;
namespace TechnicalInterview.Core.Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
             CreateMap<Account, AccountResponseDto>();
             CreateMap<DepositSpResult, DepositResponseDto>();
        }
    }
}
