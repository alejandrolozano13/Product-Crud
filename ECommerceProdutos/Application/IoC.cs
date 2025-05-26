using Application.Interfaces;
using Application.Services;
using Application.Validators;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class IoC
    {
        public static void AddApplicationInjectDependencies(this IServiceCollection services)
        {
            #region services
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region validation services
            services.AddFluentValidationAutoValidation();

            services.AddScoped<IValidator<User>, UserValidator>();
            services.AddScoped<IValidator<Product>, ProductValidator>();
            services.AddScoped<IValidator<Department>, DepartmentValidator>();
            #endregion
        }
    }
}