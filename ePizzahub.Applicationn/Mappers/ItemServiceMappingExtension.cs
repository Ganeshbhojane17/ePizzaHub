using AutoMapper;
using ePizzahub.Applicationn.DTOs.Response;
using ePizzahub.Domain.Models;

namespace ePizza.Application.Mappers
{
    public class ItemServiceMappingExtension : Profile
    {
        public ItemServiceMappingExtension()
        {
            CreateMap<ItemResponseDto, ItemDomain>().ReverseMap();
        }
    }
}
