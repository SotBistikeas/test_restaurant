using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodCost.Models.Dishes
{
    public class Dish : Entity, IAudited, IMustHaveTenant
    {

        public int TenantId { get; set; }

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
}
