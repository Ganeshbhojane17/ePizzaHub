using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzahub.Domain.Interfaces
{
    public interface IGenericRepository<TDomain> where TDomain : class
    {
        Task<IEnumerable<TDomain>> GetAllAsync();
        Task<TDomain> GetByIdAsync(object id);
        Task AddAsync(TDomain domainEntity);
        Task UpdateAsync(TDomain domainEntity, object id);
        Task DeleteAsync(object id);
        Task<bool> ExistsAsync(object id);
        Task<int> CommitAsync();
        IQueryable<TDomain> AsQueryable();
    }
}
