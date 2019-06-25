using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FoodCost.Models.VatCategories;
using FoodCost.Models.UnitOfMeasures;

namespace FoodCost.Models.Products
{
    public class Product : Entity, IAudited
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        public int VatCategoryId { get; set; }
        [ForeignKey(nameof(VatCategoryId))]
        public VatCategory VatCategory { get; set; }
        public int UnitOfMeasureId { get; set; }
        [ForeignKey(nameof(UnitOfMeasureId))]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public decimal Quantity { get; set; }


        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
