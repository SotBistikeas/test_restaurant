using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FoodCost.Models.RestaurantSettings;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCost.RestaurantSettings.Dto
{
    [AutoMap(typeof(RestaurantSetting))]
    public class RestaurantSettingDto : EntityDto
    {
        public decimal BaseFactor { get; set; }

        public decimal ExtraCostPerServing { get; set; }
    }
}
