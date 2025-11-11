using ePizzahub.Domain.Interfaces;
using ePizzahub.Domain.Models;

namespace ePizzahub.Domain.Interfaces
{
    public interface IUserTokenRepository : IGenericRepository<UserTokenDomain>
    {
        Task<UserTokenDomain> GetUserTokenAsync(int userId);

        Task<int> AddUserTokenAsync(UserTokenDomain userTokenDomain);

    }
}
