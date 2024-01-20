using AutoMapper;
using buildingBlocksCore.Models;
using buildingBlocksCore.Utils;
using Castle.Core.Resource;
using customerApi.Application.Commands;

namespace customerApi.Automapper
{
    public class RequestToResponseModelMappingProfile : Profile
    {
        public RequestToResponseModelMappingProfile()
        {
            CreateMap<InsertCustomerCommand, Cliente>()
                .ForMember(dst=>dst.CPF,map=>map.MapFrom(src=> src.CPF.OnlyNumbers()))
                ;
        }
    }
}
