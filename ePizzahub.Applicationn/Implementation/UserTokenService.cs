using AutoMapper;
using ePizzahub.Applicationn.Contracts;
using ePizzahub.Applicationn.DTOs.Request;
using ePizzahub.Domain.Interfaces;
using ePizzahub.Domain.Models;


namespace ePizzahub.Applicationn.Implementation
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IMapper _mapper;
        private readonly IUserTokenRepository _userTokenRepository;

        public UserTokenService(
            IUserTokenRepository userTokenRepository,
            IMapper mapper)
        {
            _userTokenRepository = userTokenRepository;
            _mapper = mapper;
        }
        public async Task<int> PersistToken(UserTokenRequestDto userTokenRequest)
        {
            var userTokenDomain = _mapper.Map<UserTokenDomain>(userTokenRequest);

            return await _userTokenRepository.AddUserTokenAsync(userTokenDomain);
        }
    }
}
