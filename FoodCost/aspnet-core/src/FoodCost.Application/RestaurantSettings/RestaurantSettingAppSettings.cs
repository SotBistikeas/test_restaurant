using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using FoodCost.Models.RestaurantSettings;
using FoodCost.RestaurantSettings.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCost.RestaurantSettings
{
    [AbpAuthorize]
    public class RestaurantSettingAppSettings : AsyncCrudAppService<RestaurantSetting, RestaurantSettingDto>
    {
        public RestaurantSettingAppSettings(IRepository<RestaurantSetting, int> repository) : base(repository)
        {
        }
    }
}
