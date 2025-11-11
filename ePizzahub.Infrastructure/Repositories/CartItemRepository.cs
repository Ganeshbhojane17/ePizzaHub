using AutoMapper;
using ePizzahub.Domain.Interfaces;
using ePizzahub.Domain.Models;
using ePizzahub.Infrastructure.Entities;
using ePizzahub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ePizzahub.Infrastructure.Repositories
{
    public class CartItemRepository : GenericRepository<CartItemDomain, CartItem>, ICartItemRepository
    {
        public CartItemRepository(
            EPizzadbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<CartItemDomain> GetCartItemsAsync(Guid cartId, int itemId)
        {
            var cartItemDomains = await _dbContext
                    .CartItems
                    .FirstOrDefaultAsync(
                            x => x.CartId == cartId && x.ItemId == itemId);

            return _mapper.Map<CartItemDomain>(cartItemDomains);    
        }
    }
}
