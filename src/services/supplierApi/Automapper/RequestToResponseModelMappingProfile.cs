using AutoMapper;
using buildingBlocksCore.Models;
using buildingBlocksCore.Utils;
using supplierApi.Application.Commands.Supplier;

namespace supplierApi.Automapper
{
    public class RequestToResponseModelMappingProfile : Profile
    {
        public RequestToResponseModelMappingProfile()
        {
            CreateMap<InsertSupplierCommand, Fornecedor>()
                .ForMember(dst => dst.CNPJ, map => map.MapFrom(src => src.CNPJ.OnlyNumbers()))
                ;
        }
    }
}
