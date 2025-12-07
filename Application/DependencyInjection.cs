using Application.Common;
using Application.Mappings;
using Application.Services;
using Application.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Core Services
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

        // Infrastructure Services
        services.AddScoped<ICacheService, RedisCacheService>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        // Validation
        services.AddScoped<IValidationService, ValidationService>();
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddFluentValidationAutoValidation();

        // AutoMapper
        services.AddAutoMapper(typeof(ProductProfile).Assembly);

        // MediatR
        services.AddMediatR(typeof(DependencyInjection).Assembly);

        return services;
    }
}
