using AutoMapper;
using ePizzahub.Applicationn.DTOs.Response;
using ePizzahub.Applicationn.Contracts;
using ePizzahub.Applicationn.Exceptions;
using ePizzahub.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzahub.Applicationn.Implementation
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ItemResponseDto>> GetAllItemsAsync()
        {
            var itemResponse = await _itemRepository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<ItemResponseDto>>(itemResponse);
            return result;
        }

        public async Task<ItemResponseDto> GetItemByIdAsync(int itemId)
        {
            var itemResponse = await _itemRepository.GetByIdAsync(itemId);

            if (itemResponse == null)
                throw new RecordNotFoundException($"Item with Id {itemId} doesnt exist in database");
            var result = _mapper.Map<ItemResponseDto>(itemResponse);
            return result;
        }
    }
}
