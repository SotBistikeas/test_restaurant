using Abp.Application.Services;
using Abp.Domain.Repositories;
using FoodCost.CommonServices.Dto;
using FoodCost.Models.Products;
using FoodCost.Models.VatCategories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCost.CommonServices
{
    public class VatCategoryAppService : AsyncCrudAppService<VatCategory, VatCategoryDto>
    {
        public VatCategoryAppService(IRepository<VatCategory, int> repository) : base(repository)
        {
        }
    }
}
