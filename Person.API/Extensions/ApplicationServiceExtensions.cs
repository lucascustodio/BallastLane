using Microsoft.Extensions.DependencyInjection;
using Person.Application.Services;
using Person.Application.Services.Interfaces;
using Person.Domain.Aggregates.PersonAggregate.Interface;
using Person.Domain.Aggregates.UserAggregate.Interface;
using Person.Infra.Data;
using Person.Infra.Data.Repositories;

namespace Person.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(sp =>
            {
                var connectionString = configuration.GetConnectionString("MongoDBConnection");
                return new MongoContext(connectionString);
            });

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}