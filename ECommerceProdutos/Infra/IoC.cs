using Domain.IRepositories;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra
{
    public static class IoC
    {
        public static void AddInfraInjectDependencies(this IServiceCollection services)
        {
            #region repositories
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion
        }
    }
}