using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FoodCost.Models.Dishes;

namespace FoodCost.Dishes.Dto
{
    [AutoMap(typeof(Dish_FoodIngredient))]
    public class Dish_FoodIngredientDto : EntityDto
    {
        public int FoodIngredientId { get; set; }

        public decimal Quantity { get; set; }

        public int UnitOfMeasureId { get; set; }
        public decimal Cost { get; internal set; }
    }
}
