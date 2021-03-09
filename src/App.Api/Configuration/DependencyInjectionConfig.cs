using App.Application.Interfaces;
using App.Application.Services;
using App.Data;
using App.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAwsRepository, AwsRepository>();
            services.AddScoped<ICadastroService, CadastroService>();

            return services;
        }
    }
}
