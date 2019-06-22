using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace FoodCost.EntityFrameworkCore
{
    public static class FoodCostDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<FoodCostDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<FoodCostDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
