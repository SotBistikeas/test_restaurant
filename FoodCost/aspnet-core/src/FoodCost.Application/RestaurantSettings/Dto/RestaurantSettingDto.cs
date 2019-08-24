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

        public ICollection<RestaurantExpenceDto> RestaurantExpences { get; set; }

        public decimal ExtraCostPerServing { get; set; }
        public decimal ServingsPerMonth { get; set; }
    }

    [AutoMap(typeof(RestaurantExpence))]
    public class RestaurantExpenceDto : EntityDto
    {
        public string Reason { get; set; }
        public decimal MonthlyExpence { get; set; }

    }


}
