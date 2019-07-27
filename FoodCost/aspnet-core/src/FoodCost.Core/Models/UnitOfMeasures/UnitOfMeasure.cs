using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoodCost.Models.UnitOfMeasures
{
    public class UnitOfMeasure : Entity
    {
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Symbol { get; set; }
        public UnitOfMeasureType UnitOfMeasureType { get; set; }
        public string System { get; set; }
        
        [Column(TypeName = "decimal(18, 6)")]
        public decimal BaseEquivalent { get; set; }

        public int? MeasureGroupId { get; set; }
        [ForeignKey(nameof(MeasureGroupId))]
        public MeasureGroup MeasureGroup { get; set; }

    }

    public class MeasureGroup : Entity
    {
        [StringLength(255)]
        public string Name { get; set; }

    }



    public enum UnitOfMeasureType
    {
        Length = 1,
        Area = 2,
        Volume = 3,
        Mass = 4
    }
}
