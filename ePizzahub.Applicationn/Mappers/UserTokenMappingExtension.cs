using AutoMapper;
using ePizzahub.Applicationn.DTOs.Request;
using ePizzahub.Applicationn.DTOs.Response;
using ePizzahub.Domain.Models;

namespace ePizzahub.Applicationn.Mappers
{
    public class UserTokenMappingExtension : Profile
    {
        public UserTokenMappingExtension()
        {
            CreateMap<UserTokenRequestDto, UserTokenDomain>();

            CreateMap<UserTokenDomain, RefreshTokenResponseDto>();
        }
    }
}
