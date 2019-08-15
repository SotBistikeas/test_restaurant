using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FoodCost.Models.UnitOfMeasures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodCost.Models.FoodIngredients
{
    public class FoodIngredient : Entity, IAudited, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public decimal Cost { get; set; }


        public int UnitOfMeasureId { get; set; }
        [ForeignKey(nameof(UnitOfMeasureId))]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public decimal Quantity { get; set; }

        public bool IsProduct { get; set; }

        private ICollection<FoodIngredient_Product> _foodIngredient_Product_Mapping;

        public virtual ICollection<FoodIngredient_Product> FoodIngredient_Product_Mapping
        {
            get
            {
                return this._foodIngredient_Product_Mapping ?? (this._foodIngredient_Product_Mapping = new HashSet<FoodIngredient_Product>());
            }
        }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
