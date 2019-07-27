using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using FoodCost.CommonServices.Dto;
using FoodCost.Models.UnitOfMeasures;

namespace FoodCost.CommonServices
{
    public class UnitOfMeasureAppService : AsyncCrudAppService<UnitOfMeasure, UnitOfMeasureDto>
    {
        readonly IRepository<UnitOfMeasure, int> _repository;
        public UnitOfMeasureAppService(IRepository<UnitOfMeasure, int> repository) : base(repository)
        {
            _repository = repository;
        }

        public override Task<PagedResultDto<UnitOfMeasureDto>> GetAll(PagedAndSortedResultRequestDto input)
        {
            var r = _repository.GetAll().Where(o => o.MeasureGroupId == 1 && (o.UnitOfMeasureType == UnitOfMeasureType.Mass || o.UnitOfMeasureType == UnitOfMeasureType.Volume));
            var result = new PagedResultDto<UnitOfMeasureDto>(r.Count(), ObjectMapper.Map<List<UnitOfMeasureDto>>(r));
            return Task.FromResult(result);
        }
    }
}
