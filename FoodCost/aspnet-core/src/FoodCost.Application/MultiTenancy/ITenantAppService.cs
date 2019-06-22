using Abp.Application.Services;
using Abp.Application.Services.Dto;
using FoodCost.MultiTenancy.Dto;

namespace FoodCost.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

