using ePizzahub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzahub.Domain.Interfaces
{
    public interface IItemRepository: IGenericRepository<ItemDomain>
    {
    }
}
