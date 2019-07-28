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

        public decimal FinalSalePriceExclTax { get { return UserSalePriceExclTax ?? SalePriceExclTax; } }
        public decimal FinalSalePriceInclTax { get { return UserSalePriceInclTax ?? SalePriceInclTax; } }

        public decimal BaseProfit { get { return FinalSalePriceExclTax - BaseCost; } }
        public decimal BaseProfitPerc { get { return BaseCost / BaseCost; } }

        public decimal RealProfit { get { return FinalSalePriceExclTax - RealCost; } }
        public decimal RealProfitPerc { get { return RealCost / RealCost; } }

        public decimal FoodCostPerc { get { return BaseCost / FinalSalePriceExclTax; } }


        public IList<Dish_FoodIngredientDto> FoodIngredients { get; set; }

    }
}
