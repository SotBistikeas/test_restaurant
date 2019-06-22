using System.Threading.Tasks;
using FoodCost.Configuration.Dto;

namespace FoodCost.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
