using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using FoodCost.Authorization.Users;
using FoodCost.Editions;
using FoodCost.Models.RestaurantSettings;
using FoodCost.Models.VatCategories;
using System.Threading.Tasks;

namespace FoodCost.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        private readonly IRepository<VatCategory> _vatCategoryRepository;
        private readonly IRepository<RestaurantSetting> _restaurantSettingRepository;

        public TenantManager(
            IRepository<Tenant> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore,
            IRepository<VatCategory> vatCategoryRepository,
            IRepository<RestaurantSetting> restaurantSettingRepository)
            : base(
                tenantRepository,
                tenantFeatureRepository,
                editionManager,
                featureValueStore)
        {
            _vatCategoryRepository = vatCategoryRepository;
            _restaurantSettingRepository = restaurantSettingRepository;
        }


        public async Task InitializeTenantData()
        {

            await _vatCategoryRepository.InsertAsync(new VatCategory { Name = "Κανονικό 24%", Vat = 0.24m });
            await _vatCategoryRepository.InsertAsync(new VatCategory { Name = "Μειωμένο 13%", Vat = 0.13m });
            await _vatCategoryRepository.InsertAsync(new VatCategory { Name = "Μειωμένο  6%", Vat = 0.06m });

            await _restaurantSettingRepository.InsertAsync(new RestaurantSetting
            {
                BaseFactor = 2.5m,
                ExtraCostPerServing = 3.3m
            });
        }
    }
}
