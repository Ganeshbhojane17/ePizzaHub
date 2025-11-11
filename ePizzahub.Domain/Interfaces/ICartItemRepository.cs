using ePizzahub.Domain.Interfaces;
using ePizzahub.Domain.Models;


namespace ePizzahub.Domain.Interfaces
{
    public interface ICartItemRepository : IGenericRepository<CartItemDomain>
    {
        Task<CartItemDomain> GetCartItemsAsync(Guid cartId, int itemId);
    }
}
