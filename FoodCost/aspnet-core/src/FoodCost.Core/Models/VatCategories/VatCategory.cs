using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FoodCost.Models.VatCategories
{
    public class VatCategory : Entity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }
        public decimal Vat { get; set; }
    }
}
