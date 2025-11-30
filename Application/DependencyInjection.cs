using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using Application.Common;
using Application.Validation;
using Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Application.Commands.Product; // For CreateProductCommand

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Services
            services.AddScoped<ProductService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<BrandService>();

            // Cached Services (Decorators)
            services.AddScoped<IProductService>(provider => 
                new CachedProductService(
                    provider.GetRequiredService<ProductService>(),
                    provider.GetRequiredService<ICacheService>()));

            services.AddScoped<ICategoryService>(provider => 
                new CachedCategoryService(
                    provider.GetRequiredService<CategoryService>(),
                    provider.GetRequiredService<ICacheService>()));

            services.AddScoped<IBrandService>(provider => 
                new CachedBrandService(
                    provider.GetRequiredService<BrandService>(),
                    provider.GetRequiredService<ICacheService>()));

            // Cache Service
            services.AddScoped<ICacheService, RedisCacheService>();

            // Domain Event Dispatcher
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            // Validation Service
            services.AddScoped<IValidationService, ValidationService>();

            // AutoMapper
            services.AddAutoMapper(typeof(ProductProfile).Assembly);

            // MediatR
            services.AddMediatR(typeof(CreateProductCommand).Assembly);

            // Validation
            services.AddValidatorsFromAssembly(typeof(CreateProductValidator).Assembly);
            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
