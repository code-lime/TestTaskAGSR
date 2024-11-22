using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using TaskAGSR.Application.Common.Interfaces;
using TaskAGSR.Infrastructure.DataBase;

namespace TaskAGSR.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        IConfigurationSection database = configuration.GetRequiredSection("DataBase");
        if (database.Value is not string connectionString)
        {
            MySqlConnectionStringBuilder builder = [];
            foreach (IConfigurationSection child in database.GetChildren())
                builder[child.Key] = child.Get<string>();
            connectionString = builder.ConnectionString;
        }

        return services
            .AddDbContext<ApplicationDbContext>(options => options
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .UseSnakeCaseNamingConvention())

            .AddScoped<IPatientRepository, PatientRepository>();
    }
}
