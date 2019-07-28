using Abp.Dependency;
using Abp.Domain.Repositories;
using FoodCost.Models.FoodIngredients;
using FoodCost.Models.UnitOfMeasures;
using System.Linq;

namespace FoodCost.Services
{
    public class FoodIngredientService : ITransientDependency
    {
        private IRepository<FoodIngredient> _foodIngredientRepository;
        private IRepository<FoodIngredient_Product> _foodIngredient_Product_MappingRepository;
        private IRepository<UnitOfMeasure> _unitOfMeasureRepository;

        public FoodIngredientService(IRepository<FoodIngredient> foodIngredientRepository,
            IRepository<FoodIngredient_Product> foodIngredient_Product_MappingRepository,
            IRepository<UnitOfMeasure> unitOfMeasureRepository)
        {
            _foodIngredientRepository = foodIngredientRepository;
            _foodIngredient_Product_MappingRepository = foodIngredient_Product_MappingRepository;
            _unitOfMeasureRepository = unitOfMeasureRepository;
        }
        public FoodIngredient CalculateFoodIngredient(int foodIngredientId)
        {
            var foodIngredient = _foodIngredientRepository.GetAllIncluding(o => o.FoodIngredient_Product_Mapping).FirstOrDefault(o => o.Id == foodIngredientId);
            var fip = _foodIngredient_Product_MappingRepository.GetAllIncluding(o => o.Product).Where(o => o.FoodIngredientId == foodIngredientId);
            foodIngredient.Cost = fip.Sum(o => o.Product.Quantity > 0 ? o.Product.Price / o.Product.Quantity * o.Quantity : 0);
            _foodIngredientRepository.Update(foodIngredient);
            return foodIngredient;
        }

        
    }
}
