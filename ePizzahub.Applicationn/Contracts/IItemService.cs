
using ePizzahub.Applicationn.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzahub.Applicationn.Contracts
{
    public interface IItemService
    {
        Task<IEnumerable<ItemResponseDto>> GetAllItemsAsync();
        Task <ItemResponseDto> GetItemByIdAsync(int itemId);
    }
}
