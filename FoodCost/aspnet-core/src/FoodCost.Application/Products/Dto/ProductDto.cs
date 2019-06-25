using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using FoodCost.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCost.Products.Dto
{
    [AutoMap(typeof(Product))]
    public class ProductDto : EntityDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int VatCategoryId { get; set; }

        public int UnitOfMeasureId { get; set; }
        
        public decimal Quantity { get; set; }
    }
}
