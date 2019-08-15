using System.Linq;
using FoodCost.Models.VatCategories;

namespace FoodCost.EntityFrameworkCore.Seed.Tenants
{
    public class VatCategoriesBuilder
    {
        private readonly FoodCostDbContext _context;
        private readonly int _tenantId;

        public VatCategoriesBuilder(FoodCostDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateVatCategories();
        }

        private void CreateVatCategories()
        {
            var vatCategoriesExist = _context.VatCategories.Any();
            if (vatCategoriesExist)
                return;

            _context.VatCategories.Add(new VatCategory { TenantId=_tenantId, Name = "Κανονικό 24%", Vat = 0.24m });
            _context.VatCategories.Add(new VatCategory { TenantId=_tenantId,Name = "Μειωμένο 13%", Vat = 0.13m });
            _context.VatCategories.Add(new VatCategory { TenantId=_tenantId,Name = "Μειωμένο  6%", Vat = 0.06m });

            _context.SaveChanges();

        }
    }
}
