using AutoMapper;
using ePizzahub.Applicationn.DTOs.Request;
using ePizzahub.Applicationn.DTOs.Response;
using ePizzahub.Domain.Models;

namespace ePizzahub.Applicationn.Implementation
{
    public class CartServiceMappingExtension : Profile
    {
        public CartServiceMappingExtension()
        {
            CreateMap<AddItemsDto, CartItemDomain>();
            CreateMap<AddItemsDto, CartDomain>()
                .ForMember(dest => dest.Id, 
                    opt 
                        => opt.MapFrom(src => src.CartId));

            CreateMap<CartDomain, CartResponseDto>()
                .ForMember(dest => dest.CartId , 
                    opt 
                            => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CartItems,
                    opt
                        => opt.MapFrom(src => src.ItemDomains));

            CreateMap<CartItemDomain, CartItemsResponseDto>();
        }
    }
}
