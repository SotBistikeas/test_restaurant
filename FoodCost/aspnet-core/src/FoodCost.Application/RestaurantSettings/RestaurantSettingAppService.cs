using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using FoodCost.Models.RestaurantSettings;
using FoodCost.RestaurantSettings.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCost.RestaurantSettings
{
    [AbpAuthorize]
    public class RestaurantSettingAppService : AsyncCrudAppService<RestaurantSetting, RestaurantSettingDto>
    {
        private readonly IRepository<RestaurantExpence> _restaurantExpencesRepository;

        public RestaurantSettingAppService(IRepository<RestaurantSetting, int> repository,
            IRepository<RestaurantExpence> restaurantExpencesRepository) : base(repository)
        {
            _restaurantExpencesRepository = restaurantExpencesRepository;
        }


        protected override IQueryable<RestaurantSetting> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
        {
            return Repository.GetAllIncluding(p => p.RestaurantExpences);
        }

        protected override async Task<RestaurantSetting> GetEntityByIdAsync(int id)
        {
            var entity = Repository.GetAllIncluding(p => p.RestaurantExpences).FirstOrDefault(p => p.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(RestaurantSetting), id);
            }

            return await Task.FromResult(entity);
        }

        [RemoteService(false)]
        public override Task Delete(EntityDto<int> input)
        {
            throw new AbpAuthorizationException();
        }

        [RemoteService(false)]
        public override Task<RestaurantSettingDto> Get(EntityDto<int> input)
        {
            throw new AbpAuthorizationException();
        }

        [RemoteService(false)]
        public override Task<PagedResultDto<RestaurantSettingDto>> GetAll(PagedAndSortedResultRequestDto input)
        {
            throw new AbpAuthorizationException();
        }


        [RemoteService(false)]
        public override Task<RestaurantSettingDto> Create(RestaurantSettingDto input)
        {
            return base.Create(input);
        }

        public async Task<RestaurantSettingDto> Get()
        {
            CheckGetPermission();
            var entity = Repository.GetAllIncluding(p => p.RestaurantExpences).FirstOrDefault(p => p.TenantId == AbpSession.TenantId);
            if (entity == null)
            {
                throw new AbpAuthorizationException();
            }

            return await Task.FromResult(ObjectMapper.Map<RestaurantSettingDto>(entity));
        }


        public override async Task<RestaurantSettingDto> Update(RestaurantSettingDto input)
        {

            CheckUpdatePermission();

            var entity = await GetEntityByIdAsync(input.Id);

            //Dont use auto mapping here
            entity.BaseFactor = input.BaseFactor;
            entity.ServingsPerMonth = input.ServingsPerMonth > 0 ? input.ServingsPerMonth : 1;
            entity.ExtraCostPerServing = input.RestaurantExpences.Sum(o => o.MonthlyExpence) / input.ServingsPerMonth;

            foreach (var child in input.RestaurantExpences)
            {
                // Check to see if this is a new child item
                if (entity.RestaurantExpences.All(x => x.Id != child.Id))
                {

                    // Map the DTO to child entity and add it to the collection
                    var item = ObjectMapper.Map<RestaurantExpence>(child);
                    child.Id = await _restaurantExpencesRepository.InsertAndGetIdAsync(item);
                    entity.RestaurantExpences.Add(item);
                    continue;
                }

                // Check to see if this is an existing child item
                var existingChild = entity.RestaurantExpences.FirstOrDefault(x => x.Id == child.Id);
                if (existingChild == null)
                {
                    continue;
                }


                // It's safe to use AutoMapper to map the child entity and avoid updating properties manually, 
                // provided that it doesn't have child-items of their own
                ObjectMapper.Map(child, existingChild);
            }

            // Find which of the child entities should be deleted
            // entity.IsTransient() is an extension method which returns true if the entity has just been added
            foreach (var child in entity.RestaurantExpences.Where(x => !x.IsTransient()).ToList())
            {
                if (input.RestaurantExpences.Any(x => x.Id == child.Id))
                {
                    continue;
                }

                // We don't have this entity in the list sent by the client.
                // That means we should delete it
                await _restaurantExpencesRepository.DeleteAsync(child);
                entity.RestaurantExpences.Remove(child);
            }


            // And finally, call the repository update and return the result mapped to DTO
            await Repository.InsertOrUpdateAndGetIdAsync(entity);


            return MapToEntityDto(entity);
        }


    }
}
