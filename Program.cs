using Infrastructure.Data;
using Domain.Entities;
using Presentation.Dto.Category;
using Presentation.Dto.Product;
using Application.Services;
using Application.Common;
using Application.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Application.Mappings;
using Application;
using Infrastructure.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
    options.InstanceName = "StoreAPI_";
});

// Database
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cache Service
builder.Services.AddScoped<ICacheService, RedisCacheService>();

// Domain Event Dispatcher
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IProductService>(provider => 
    new CachedProductService(
        provider.GetRequiredService<ProductService>(),
        provider.GetRequiredService<ICacheService>()));

builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ICategoryService>(provider => 
    new CachedCategoryService(
        provider.GetRequiredService<CategoryService>(),
        provider.GetRequiredService<ICacheService>()));

builder.Services.AddScoped<IValidationService, ValidationService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddAutoMapper(typeof(CategoryProfile));

// MediatR
builder.Services.AddMediatR(typeof(Application.Commands.Product.CreateProductCommand).Assembly);

// Validation
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Add global exception handling middleware first
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

