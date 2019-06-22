using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using FoodCost.Configuration.Dto;

namespace FoodCost.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : FoodCostAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
