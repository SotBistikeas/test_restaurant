using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using FoodCost.Authorization.Roles;
using FoodCost.Authorization.Users;
using FoodCost.MultiTenancy;
using FoodCost.Models.Products;

namespace FoodCost.EntityFrameworkCore
{
    public class FoodCostDbContext : AbpZeroDbContext<Tenant, Role, User, FoodCostDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<Product> Products { get; set; }
        public DbSet<VatCategory> VatCategories { get; set; }

        public FoodCostDbContext(DbContextOptions<FoodCostDbContext> options)
            : base(options)
        {
        }
    }
}
