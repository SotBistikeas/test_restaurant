using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FoodCost.Models.Dishes;
using System.Collections.Generic;

namespace FoodCost.Dishes.Dto
{
    [AutoMap(typeof(Dish))]
    public class DishFullDto : EntityDto
    {
        public string Name { get; set; }

        public decimal BaseCost { get; set; }
        public decimal RealCost { get; set; }

        public decimal SalePriceExclTax { get; set; }
        public decimal SalePriceInclTax { get; set; }


        public decimal? UserSalePriceExclTax { get; set; }
        public decimal? UserSalePriceInclTax { get; set; }

        public decimal FinalSalePriceExclTax { get; set; }
        public decimal FinalSalePriceInclTax { get; set; }

        public decimal BaseProfit { get; set; }
        public decimal BaseProfitPerc { get; set; }

        public decimal RealProfit { get; set; }
        public decimal RealProfitPerc { get; set; }

        public decimal FoodCostPerc { get; set; }


        public IList<Dish_FoodIngredientDto> FoodIngredients { get; set; }

    }
}
