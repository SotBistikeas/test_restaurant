using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FoodCost.Models.Dishes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCost.Dishes.Dto
{
    [AutoMap(typeof(Dish))]
    public class DishDto : EntityDto
    {
        public string Name { get; set; }
    }

    [AutoMap(typeof(Dish_FoodIngredient))]
    public class Dish_FoodIngredientDto : EntityDto
    {
        public int FoodIngredientId { get; set; }

        public decimal Quantity { get; set; }

        public int UnitOfMeasureId { get; set; }
    }
}
