using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using FoodCost.Authorization.Roles;
using FoodCost.Authorization.Users;
using FoodCost.MultiTenancy;
using FoodCost.Models.Products;
using FoodCost.Models.VatCategories;
using FoodCost.Models.UnitOfMeasures;

namespace FoodCost.EntityFrameworkCore
{
    public class FoodCostDbContext : AbpZeroDbContext<Tenant, Role, User, FoodCostDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<Product> Products { get; set; }
        public DbSet<VatCategory> VatCategories { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public DbSet<MeasureGroup> MeasureGroups { get; set; }

        public FoodCostDbContext(DbContextOptions<FoodCostDbContext> options)
            : base(options)
        {
        }
    }
}
