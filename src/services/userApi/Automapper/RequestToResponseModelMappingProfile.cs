using AutoMapper;
using buildingBlocksCore.Models.Dto;
using buildingBlocksCore.Models.Request;
using Microsoft.AspNetCore.Identity;

namespace userApi.Automapper
{
    public class RequestToResponseModelMappingProfile : Profile
    {
        public RequestToResponseModelMappingProfile()
        {
            CreateMap<UserRegisterRequest, UserRegisterDto>();
            CreateMap<UserUpdateRequest, UserUpdateDto>();
            CreateMap<IdentityUser, UserUpdateDto>();
            CreateMap<IdentityUser, UserRegisterDto>();

        }
    }
}
