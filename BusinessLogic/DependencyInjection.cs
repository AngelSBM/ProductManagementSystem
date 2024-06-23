using BusinessLogic.Helpers;
using BusinessLogic.Services;
using DataAccess.Repositories;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddAutoMapper(typeof(AppMapper));
            return services;
        }
    }
}
