using AutoMapper;
using TechnicalInterview.Core.Application.Dtos;
using TechnicalInterview.WebAPI.Dtos.Response;

namespace TechnicalInterview.Core.Application.Mappings
{
    public class TransferProfile : Profile
    {
        public TransferProfile()
        {
            CreateMap<TransferDto, TransferResponse>();
        }       
    }
}
