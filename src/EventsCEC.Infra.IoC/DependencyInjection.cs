using EventsCEC.Application.Interfaces;
using EventsCEC.Application.Mappings;
using EventsCEC.Application.Services;
using EventsCEC.Domain.Repositories;
using EventsCEC.Infra.Data.Context;
using EventsCEC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventsCEC.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<AppDbContext>();

            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IUserManagerService, UserManagerService>();

            services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
            services.AddScoped<IUserManagerRepository, UserManagerRepository>();
            services.AddScoped<ISeedUserRoleInitialRepository, SeedUserRoleInitialRepository>();

            services.AddAutoMapper(typeof(DomainToDtoMappingProfile));

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            return services;
        }
    }
}
