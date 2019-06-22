using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using FoodCost.Configuration;
using FoodCost.Web;

namespace FoodCost.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class FoodCostDbContextFactory : IDesignTimeDbContextFactory<FoodCostDbContext>
    {
        public FoodCostDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FoodCostDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            FoodCostDbContextConfigurer.Configure(builder, configuration.GetConnectionString(FoodCostConsts.ConnectionStringName));

            return new FoodCostDbContext(builder.Options);
        }
    }
}
