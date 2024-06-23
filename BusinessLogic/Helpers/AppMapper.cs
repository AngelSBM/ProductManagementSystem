using AutoMapper;
using Domain.Entities;
using ProductManagementSystem.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
