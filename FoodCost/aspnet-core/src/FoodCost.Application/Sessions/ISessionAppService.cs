using System.Threading.Tasks;
using Abp.Application.Services;
using FoodCost.Sessions.Dto;

namespace FoodCost.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
