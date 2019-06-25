using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodCost.Models.UnitOfMeasures;
using System.Collections.Generic;

namespace FoodCost.Models.Products
{
    public class FoodIngredient : Entity, IAudited
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public decimal Cost { get; set; }


        public int UnitOfMeasureId { get; set; }
        [ForeignKey(nameof(UnitOfMeasureId))]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public decimal Quantity { get; set; }


        public ICollection<FoodIngredient_Product> FoodIngredient_Product_Mapping { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }

    public class FoodIngredient_Product : Entity, IAudited
    {
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
