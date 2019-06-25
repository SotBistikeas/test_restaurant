using System.Linq;
using Microsoft.EntityFrameworkCore;
using FoodCost.Models.UnitOfMeasures;

namespace FoodCost.EntityFrameworkCore.Seed.Tenants
{
    public class UnitOfMeasuresBuilder
    {
        private readonly FoodCostDbContext _context;
        private readonly int _tenantId;

        public UnitOfMeasuresBuilder(FoodCostDbContext context, int tenantId)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUnitOfMeasures();
        }

        private void CreateUnitOfMeasures()
        {
            var unitOfMeasuresExist = _context.UnitOfMeasures.Any();
            if (unitOfMeasuresExist)
                return;

            var metric = _context.MeasureGroups.Add(new MeasureGroup { Name = "Metric" }).Entity;
            var imperial = _context.MeasureGroups.Add(new MeasureGroup { Name = "Imperial" }).Entity;
            _context.SaveChanges();


            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 101, MeasureGroup = metric, UnitOfMeasureType = UnitOfMeasureType.Length, System = "Metric", Name = "Meter", Symbol = "m", BaseEquivalent = 1.0m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 102, MeasureGroup = metric, UnitOfMeasureType = UnitOfMeasureType.Length, System = "Metric", Name = "Millimeter", Symbol = "mm", BaseEquivalent = 0.001m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 103, MeasureGroup = metric, UnitOfMeasureType = UnitOfMeasureType.Length, System = "Metric", Name = "Centimeter", Symbol = "cm", BaseEquivalent = 0.01m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 104, MeasureGroup = null, UnitOfMeasureType = UnitOfMeasureType.Length, System = "Metric", Name = "Decimeter", Symbol = "dm", BaseEquivalent = 0.1m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 105, MeasureGroup = null, UnitOfMeasureType = UnitOfMeasureType.Length, System = "Metric", Name = "Kilometer", Symbol = "km", BaseEquivalent = 1000.0m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 106, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Length, System = "Imperial / USCS", Name = "Inch", Symbol = "in", BaseEquivalent = 0.254m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 107, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Length, System = "Imperial / USCS", Name = "Foot", Symbol = "ft", BaseEquivalent = 0.3048m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 108, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Length, System = "Imperial / USCS", Name = "Yard", Symbol = "yd", BaseEquivalent = 0.9144m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 109, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Length, System = "Imperial / USCS", Name = "Mile", Symbol = "mi", BaseEquivalent = 1609.0m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 110, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Length, System = "Imperial", Name = "Nautical Mile", Symbol = "sm", BaseEquivalent = 1852.0m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 201, MeasureGroup = metric, UnitOfMeasureType = UnitOfMeasureType.Area, System = "Metric", Name = "Square meter", Symbol = "m²", BaseEquivalent = 1.0m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 202, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Area, System = "Imperial / USCS", Name = "Acre", Symbol = "acre", BaseEquivalent = 4046.873m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 203, MeasureGroup = null, UnitOfMeasureType = UnitOfMeasureType.Area, System = "Metric", Name = "Are", Symbol = "ares", BaseEquivalent = 100.0m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 204, MeasureGroup = null, UnitOfMeasureType = UnitOfMeasureType.Area, System = "Metric", Name = "Hectare", Symbol = "ha", BaseEquivalent = 10000.0m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 205, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Area, System = "Imperial", Name = "Square inches", Symbol = "in²", BaseEquivalent = 0.00064516m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 206, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Area, System = "Imperial", Name = "Square feet", Symbol = "ft²", BaseEquivalent = 0.093m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 207, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Area, System = "Imperial", Name = "Square yards", Symbol = "yd²", BaseEquivalent = 0.836m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 208, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Area, System = "Imperial", Name = "Square miles", Symbol = "mi²", BaseEquivalent = 2590000.0m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 301, MeasureGroup = metric, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Metric", Name = "Cubic meter", Symbol = "m³", BaseEquivalent = 1000.0m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 302, MeasureGroup = metric, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Metric", Name = "Liter", Symbol = "l", BaseEquivalent = 1.0m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 303, MeasureGroup = metric, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Metric", Name = "Milliliter", Symbol = "ml", BaseEquivalent = 0.001m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 304, MeasureGroup = metric, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Metric", Name = "Centiliter", Symbol = "cl", BaseEquivalent = 0.01m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 305, MeasureGroup = metric, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Metric", Name = "Deciliter", Symbol = "dl", BaseEquivalent = 0.1m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 306, MeasureGroup = metric, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Metric", Name = "Hectoliter", Symbol = "hl", BaseEquivalent = 100.0m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 307, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "USCS", Name = "Cubic Inch", Symbol = "in³", BaseEquivalent = 0.016387m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 308, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "USCS", Name = "Cubic Foot", Symbol = "ft³", BaseEquivalent = 28.317m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 309, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "USCS", Name = "Cubic Yard", Symbol = "yd³", BaseEquivalent = 764.555m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 310, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS", Name = "Teaspoon UK", Symbol = "tsp", BaseEquivalent = 0.00591939m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 311, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS", Name = "Teaspoon US", Symbol = "tsp", BaseEquivalent = 0.00493m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 312, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS", Name = "Tablespoon UK", Symbol = "tbsp", BaseEquivalent = 0.0177582m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 313, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS", Name = "Tablespoon US", Symbol = "tbsp", BaseEquivalent = 0.0148m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 314, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS", Name = "Tablespoon Australia", Symbol = "tbsp", BaseEquivalent = 0.02m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 315, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS", Name = "Tablespoon Canada", Symbol = "tbsp", BaseEquivalent = 0.015m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 316, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS", Name = "Fluid ounce UK", Symbol = "fl oz", BaseEquivalent = 0.283872m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 317, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS", Name = "Fluid ounce US", Symbol = "fl oz", BaseEquivalent = 0.2957m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 318, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS UK", Name = "Cup", Symbol = "cup", BaseEquivalent = 0.28413m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 319, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS US", Name = "Cup", Symbol = "cup", BaseEquivalent = 0.23659m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 320, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS Australia/Canada", Name = "Cup", Symbol = "cup", BaseEquivalent = 0.25m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 321, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS UK", Name = "Gill", Symbol = "gill", BaseEquivalent = 0.142m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 322, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS US liquid", Name = "Gill", Symbol = "gill", BaseEquivalent = 0.118m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 323, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS US dry", Name = "Gill", Symbol = "gill", BaseEquivalent = 0.138m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 324, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS UK", Name = "Pint", Symbol = "pt", BaseEquivalent = 0.568m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 325, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS US liquid", Name = "Pint", Symbol = "pt", BaseEquivalent = 0.473m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 326, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS US dry", Name = "Pint", Symbol = "pt", BaseEquivalent = 0.551m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 327, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS UK", Name = "Quart", Symbol = "qt", BaseEquivalent = 1.14m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 328, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS US liquid", Name = "Quart", Symbol = "qt", BaseEquivalent = 0.946m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 329, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS US dry", Name = "Quart", Symbol = "qt", BaseEquivalent = 1.101m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 330, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS UK", Name = "Gallon", Symbol = "gal", BaseEquivalent = 4.55m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 331, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS US liquid", Name = "Gallon", Symbol = "gal", BaseEquivalent = 3.78m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 332, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Volume, System = "Imperial / USCS US dry", Name = "Gallon", Symbol = "gal", BaseEquivalent = 4.4m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 401, MeasureGroup = metric, UnitOfMeasureType = UnitOfMeasureType.Mass, System = "Metric ", Name = "Grams", Symbol = "g", BaseEquivalent = 1.0m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 402, MeasureGroup = metric, UnitOfMeasureType = UnitOfMeasureType.Mass, System = "Metric ", Name = "Kilogram", Symbol = "kg", BaseEquivalent = 1000.0m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 403, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Mass, System = "Imperial / USCS", Name = "Grain", Symbol = "gr", BaseEquivalent = 0.06479886m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 404, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Mass, System = "Imperial / USCS", Name = "Dram", Symbol = "dr", BaseEquivalent = 1.77184375m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 405, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Mass, System = "Imperial / USCS", Name = "Ounce", Symbol = "oz", BaseEquivalent = 28.3495m });
            _context.UnitOfMeasures.Add(new UnitOfMeasure { Id = 406, MeasureGroup = imperial, UnitOfMeasureType = UnitOfMeasureType.Mass, System = "Imperial / USCS", Name = "Pound", Symbol = "lb", BaseEquivalent = 453.592m });


            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT UnitOfMeasures ON");
            _context.SaveChanges();
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT UnitOfMeasures OFF");

        }
    }
}
