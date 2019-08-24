using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using FoodCost.Authorization.Roles;
using FoodCost.Authorization.Users;
using FoodCost.Models.Dishes;
using FoodCost.Models.FoodIngredients;
using FoodCost.Models.Products;
using FoodCost.Models.RestaurantSettings;
using FoodCost.Models.UnitOfMeasures;
using FoodCost.Models.VatCategories;
using FoodCost.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace FoodCost.EntityFrameworkCore
{
    public class FoodCostDbContext : AbpZeroDbContext<Tenant, Role, User, FoodCostDbContext>, IAbpPersistedGrantDbContext
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<Product> Products { get; set; }
        public DbSet<VatCategory> VatCategories { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public DbSet<MeasureGroup> MeasureGroups { get; set; }
        public DbSet<FoodIngredient> FoodIngredients { get; set; }
        public DbSet<FoodIngredient_Product> FoodIngredient_Product_Mapping { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Dish_FoodIngredient> Dish_FoodIngredient_Mapping { get; set; }
        public DbSet<PersistedGrantEntity> PersistedGrants { get; set; }
        public DbSet<RestaurantSetting> RestaurantSettings { get; set; }
        public DbSet<RestaurantExpence> RestaurantExpence { get; set; }

        public FoodCostDbContext(DbContextOptions<FoodCostDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
