using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // nel mapping ignora il campo Id
            Mapper.CreateMap<Customer, CustomerDto>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<CustomerDto, Customer>();
            // nel mapping ignora il campo Id
            Mapper.CreateMap<Movie, MovieDto>().ForMember(m => m.Id, opt => opt.Ignore()); ;
            Mapper.CreateMap<MovieDto, Movie>();
        }
    }
}