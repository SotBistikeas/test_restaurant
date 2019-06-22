using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FoodCost.Configuration;

namespace FoodCost.Web.Host.Startup
{
    [DependsOn(
       typeof(FoodCostWebCoreModule))]
    public class FoodCostWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public FoodCostWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FoodCostWebHostModule).GetAssembly());
        }
    }
}
