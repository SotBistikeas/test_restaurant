using Abp;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using FoodCost.Models.Products;
using FoodCost.Products.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCost.Products
{
    public class ProductAppService : AsyncCrudAppService<Product, ProductDto>
    {
        public ProductAppService(IRepository<Product, int> repository) : base(repository)
        {
        }
    }
}
