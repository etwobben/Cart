using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CartWebApi.FilterAttributes;
using CartWebApi.Services;
using Data;
using Data.Repositories;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CartWebApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        private static bool _autoMapperIsConfigured;

        public static IServiceCollection AddFilterAttributes(this IServiceCollection services)
        {
            return services
                .AddScoped<ModelValidationAttribute>();
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ProductResourceService>()
                .AddScoped<CartLineResourceService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<ICartLineService, CartLineService>();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IRepository<Product>, ProductRepository>()
                .AddScoped<IRepository<CartLine>, CartRepository>();
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            return services.AddDbContext<DatabaseContext>(options =>
                options.UseInMemoryDatabase("InMemoryDb")
            );
        }

        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection collection)
        {
            if (_autoMapperIsConfigured) return collection;

            var assembly = System.Reflection.Assembly.GetEntryAssembly();

            collection.AddAutoMapper(config =>
            {
                //scan for any classes that implement Profile, and add them to the AutoMapper configuration
                foreach (var typeInfo in assembly.DefinedTypes)
                {
                    if (!typeInfo.ImplementedInterfaces.Contains(typeof(Profile)))
                    {
                        continue;
                    }

                    config.AddProfile(typeInfo);
                }
            });

            _autoMapperIsConfigured = true;

            return collection;
        }
    }
}
