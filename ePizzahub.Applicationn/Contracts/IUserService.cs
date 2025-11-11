using ePizzahub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzahub.Applicationn.Contracts
{
    public interface IUserService
    {
        Task<UserDomain> GetUserDetailsAsync(string emailAddress);
        Task<UserTokenDomain> GetUserTokenAsync(int userId);
    }
}
