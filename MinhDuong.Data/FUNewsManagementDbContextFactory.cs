using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace MinhDuong.Data
{
    public class FUNewsManagementDbContextFactory : IDesignTimeDbContextFactory<FUNewsManagementDbContext>
    {
        public FUNewsManagementDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FUNewsManagementDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new FUNewsManagementDbContext(optionsBuilder.Options, configuration);
        }
    }
}