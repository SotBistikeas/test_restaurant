using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FoodCost.Models.Products
{
    public class VatCategory : Entity
    {

        [StringLength(255)]
        public string Name { get; set; }
        public decimal Vat { get; set; }
    }
}
