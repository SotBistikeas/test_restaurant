using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FoodCost.Models.Products;

namespace FoodCost.Products.Dto
{
    [AutoMap(typeof(FoodIngredient))]
    public class FoodIngredientDto : EntityDto
    {
        public string Name { get; set; }

        public decimal Cost { get; set; }

        public int UnitOfMeasureId { get; set; }

        public decimal Quantity { get; set; }
    }

    [AutoMap(typeof(FoodIngredient_Product))]
    public class FoodIngredient_ProductDto : EntityDto
    {
        public int ProductId { get; set; }

        public decimal Quantity { get; set; }

        public int UnitOfMeasureId { get; set; }
    }
}
