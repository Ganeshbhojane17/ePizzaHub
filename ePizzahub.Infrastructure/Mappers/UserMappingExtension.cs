using AutoMapper;
using ePizzahub.Domain.Models;
using ePizzahub.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzahub.Infrastructure.Mappers
{
    public class UserMappingExtension : Profile
    {
        public UserMappingExtension() 
        { 
            CreateMap<User, UserDomain>().ReverseMap();
        }
    }
}
