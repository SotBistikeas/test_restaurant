using Abp.Application.Services;
using Abp.Domain.Repositories;
using FoodCost.CommonServices.Dto;
using FoodCost.Models.UnitOfMeasures;

namespace FoodCost.CommonServices
{
    public class UnitOfMeasureAppService : AsyncCrudAppService<UnitOfMeasure, UnitOfMeasureDto>
    {
        public UnitOfMeasureAppService(IRepository<UnitOfMeasure, int> repository) : base(repository)
        {
        }
    }
}
