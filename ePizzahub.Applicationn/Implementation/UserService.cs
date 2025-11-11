using ePizzahub.Applicationn.Contracts;
using ePizzahub.Domain.Interfaces;
using ePizzahub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzahub.Applicationn.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _userTokenRepository;

        public UserService(IUserRepository userRepository,
            IUserTokenRepository userTokenRepository)
        {
            _userRepository = userRepository;
            this._userTokenRepository = userTokenRepository;
        }



        public async Task<UserDomain> GetUserDetailsAsync(string emailAddress)
        {
            return await _userRepository.GetUserByEmailAsync(emailAddress);
        }

        public async Task<UserTokenDomain> GetUserTokenAsync(int userId)
        {
            return await _userTokenRepository.GetUserTokenAsync(userId);
        }
    }
}
