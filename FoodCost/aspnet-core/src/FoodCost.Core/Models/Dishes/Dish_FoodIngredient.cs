using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FoodCost.Models.FoodIngredients;
using FoodCost.Models.UnitOfMeasures;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodCost.Models.Dishes
{
    public class Dish_FoodIngredient : Entity, IAudited, IMustHaveTenant
    {
        public int TenantId { get; set; }

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
