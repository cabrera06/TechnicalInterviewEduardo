using AutoMapper;
using TechnicalInterview.Core.Application.Dtos;
using TechnicalInterview.Core.Domain.Models;
namespace TechnicalInterview.Core.Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
             CreateMap<Account, AccountDto>();
        }
    }
}
