using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FoodCost.Models.Dishes;
using System;
using System.Text;

namespace FoodCost.Dishes.Dto
{
    [AutoMap(typeof(Dish))]
    public class DishDto : EntityDto
    {
        public string Name { get; set; }


    }
}
