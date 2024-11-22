using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace TaskAGSR.Infrastructure.DataBase;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    private static DbContextOptionsBuilder<ApplicationDbContext> CreateOptions(DbContextOptionsBuilder<ApplicationDbContext> builder)
        => builder
            .UseMySql(ServerVersion.Create(11, 2, 2, ServerType.MariaDb))
            .UseSnakeCaseNamingConvention();

    public ApplicationDbContext CreateDbContext(string[] args)
        => new ApplicationDbContext(CreateOptions(new DbContextOptionsBuilder<ApplicationDbContext>()).Options);
}
