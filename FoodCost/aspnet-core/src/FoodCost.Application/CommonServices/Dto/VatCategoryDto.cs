﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FoodCost.Models.Products;
using FoodCost.Models.VatCategories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCost.CommonServices.Dto
{
    [AutoMap(typeof(VatCategory))]
    public class VatCategoryDto : EntityDto
    {
        public string Name { get; set; }
        public decimal Vat { get; set; }
    }
}
