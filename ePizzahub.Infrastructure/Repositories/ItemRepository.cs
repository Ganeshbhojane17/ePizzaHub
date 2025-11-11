using AutoMapper;
using ePizzahub.Domain.Interfaces;
using ePizzahub.Domain.Models;
using ePizzahub.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzahub.Infrastructure.Repositories
{
    public class ItemRepository : GenericRepository<ItemDomain, Item>, IItemRepository
    {
        public ItemRepository(EPizzadbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

       
    }
}
