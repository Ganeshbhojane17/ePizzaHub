using AutoMapper;
using ePizzahub.Domain.Interfaces;
using ePizzahub.Domain.Models;
using ePizzahub.Infrastructure.Entities;
using ePizzahub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace ePizzahub.Infrastructure.Repositories
{
    public class UserTokenRepository : GenericRepository<UserTokenDomain, UserToken>, IUserTokenRepository
    {
        private readonly IConfiguration _configuration;
        public UserTokenRepository(EPizzadbContext dbContext, IMapper mapper, IConfiguration configuration) : base(dbContext, mapper)
        {
            _configuration = configuration;
        }

        public async Task<UserTokenDomain> GetUserTokenAsync(int userId)
        {
            var userToken
                = await _dbContext.UserTokens.FirstOrDefaultAsync(x => x.UserId == userId);

            return _mapper.Map<UserTokenDomain>(userToken);
        }

        //public async Task<int> AddUserTokenAsync(UserTokenDomain userTokenDomain)
        //{
        //    var tokenDetails
        //        = await _dbContext.UserTokens.Where(x => x.UserId == userTokenDomain.UserId).ToListAsync();

        //    if (tokenDetails.Any())
        //        _dbContext.UserTokens.RemoveRange(tokenDetails);

        //    await _dbContext.AddAsync(_mapper.Map<UserToken>(userTokenDomain));

        //    return await _dbContext.SaveChangesAsync();
        //}

        public async Task<int> AddUserTokenAsync(UserTokenDomain userTokenDomain)
        {
            // remove existing tokens for the user
            var existingTokens = await _dbContext.UserTokens.Where(x => x.UserId == userTokenDomain.UserId).ToListAsync();

            if (existingTokens.Any())
                _dbContext.UserTokens.RemoveRange(existingTokens);

            // Read expiry days from configuration (if you still want that)
            var raw = _configuration["Jwt:RefreshTokenExpiryInDays"];
            if (!int.TryParse(raw, out var expiryDays))
                expiryDays = 7;

            // Manually map domain → entity
            var userToken = new UserToken
            {
                Id = userTokenDomain.Id,
                UserId = userTokenDomain.UserId,
                AccessToken = userTokenDomain.AccessToken,
                RefreshToken = userTokenDomain.RefreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(expiryDays)
            };

            await _dbContext.UserTokens.AddAsync(userToken);
            return await _dbContext.SaveChangesAsync();
        }

    }
}
