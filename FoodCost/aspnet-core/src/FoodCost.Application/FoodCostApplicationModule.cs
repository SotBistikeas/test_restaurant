using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FoodCost.Authorization;

namespace FoodCost
{
    [DependsOn(
        typeof(FoodCostCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class FoodCostApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<FoodCostAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(FoodCostApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
