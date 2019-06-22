using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace FoodCost.Controllers
{
    public abstract class FoodCostControllerBase: AbpController
    {
        protected FoodCostControllerBase()
        {
            LocalizationSourceName = FoodCostConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
