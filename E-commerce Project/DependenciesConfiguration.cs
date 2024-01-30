using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models_Logic;
using E_commerce.Logic.Models_Logic.Cryptography;
using E_commerce.Logic.Models_Logic.Table_Repo;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Project
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection GetConfig(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DBcontext>(con => con
                                                     .UseSqlServer(configuration.GetConnectionString("Connection"))
                                                     .EnableSensitiveDataLogging()

                                            );

            return services;

        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDataCollection, DataCollection>();
            return services;

        }
    }
}
