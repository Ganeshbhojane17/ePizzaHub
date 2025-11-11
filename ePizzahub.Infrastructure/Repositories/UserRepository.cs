using AutoMapper;
using ePizzahub.Domain.Interfaces;
using ePizzahub.Domain.Models;
using ePizzahub.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzahub.Infrastructure.Repositories
{
    public class UserRepository: GenericRepository<UserDomain, User>, IUserRepository
    {
        public UserRepository(EPizzadbContext dbContext, IMapper mapper): base(dbContext, mapper)
        {
            
        }

        public async Task<UserDomain> GetUserByEmailAsync(string emailAddress)
        {
            var userDetails
                 = await _dbContext.Users
                 .FirstOrDefaultAsync(x => x.Email == emailAddress);

            return _mapper.Map<UserDomain>(userDetails);

        }
    }
}
