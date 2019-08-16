using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCost.Models.RestaurantSettings
{
    public class RestaurantSetting : Entity, IAudited, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public decimal BaseFactor { get; set; }

        public decimal ExtraCostPerServing { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
