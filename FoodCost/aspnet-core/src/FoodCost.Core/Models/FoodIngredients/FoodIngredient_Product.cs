using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FoodCost.Models.Products;
using FoodCost.Models.UnitOfMeasures;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodCost.Models.FoodIngredients
{
    public class FoodIngredient_Product : Entity, IAudited, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public int FoodIngredientId { get; set; }
        [ForeignKey(nameof(FoodIngredientId))]
        public FoodIngredient FoodIngredient { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

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
