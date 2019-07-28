using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FoodCost.Models.Products;
using FoodCost.Models.UnitOfMeasures;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCost.CommonServices.Dto
{
    [AutoMap(typeof(UnitOfMeasure))]
    public class UnitOfMeasureDto : EntityDto
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public UnitOfMeasureType UnitOfMeasureType { get; set; }
    }
}
