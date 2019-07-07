using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FoodCost.Models.Products;
using FoodCost.Models.UnitOfMeasures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodCost.Models.Dishes
{
    public class Dish : Entity, IAudited
    {
        [Required]
        [StringLength(255)]
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


        public ICollection<Dish_FoodIngredient> Dish_FoodIngredient_Mapping { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }

    public class Dish_FoodIngredient : Entity, IAudited
    {

        public int DishId { get; set; }
        [ForeignKey(nameof(DishId))]
        public Dish Dish { get; set; }

        public int FoodIngredientId { get; set; }
        [ForeignKey(nameof(FoodIngredientId))]
        public FoodIngredient FoodIngredient { get; set; }


        public decimal Quantity { get; set; }

        public int UnitOfMeasureId { get; set; }
        [ForeignKey(nameof(UnitOfMeasureId))]
        public UnitOfMeasure UnitOfMeasure { get; set; }


        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
