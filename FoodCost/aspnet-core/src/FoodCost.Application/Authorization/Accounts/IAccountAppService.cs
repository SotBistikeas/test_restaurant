using System.Threading.Tasks;
using Abp.Application.Services;
using FoodCost.Authorization.Accounts.Dto;

namespace FoodCost.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
