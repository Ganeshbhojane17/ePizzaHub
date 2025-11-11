using AutoMapper;
using ePizzahub.Applicationn.DTOs.Response;
using ePizzahub.Domain.Models;
using ePizzahub.Infrastructure.Entities;

namespace ePizza.Infrastructure.Mappers
{
    internal class ItemMappingExtensions : Profile
    {
        public ItemMappingExtensions()
        {
            CreateMap<ItemDomain, Item>().ReverseMap();

            CreateMap<Item, ItemResponseDto>().ReverseMap();
            CreateMap<ItemDomain, ItemResponseDto>().ReverseMap();
        }
    }
}
