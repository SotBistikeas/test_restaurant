using Abp.Dependency;
using Abp.Domain.Repositories;
using FoodCost.Models.Dishes;
using System.Linq;

namespace FoodCost.Services
{
    public class DishService : ITransientDependency
    {
        private IRepository<Dish> _dishRepository;
        private IRepository<Dish_FoodIngredient> _dish_FoodIgredient_MappingRepository;

        public DishService(IRepository<Dish> dishRepository, IRepository<Dish_FoodIngredient> dish_FoodIgredient_MappingRepository)
        {
            _dishRepository = dishRepository;
            _dish_FoodIgredient_MappingRepository = dish_FoodIgredient_MappingRepository;
        }
        public Dish CalculateDish(int dishId)
        {
            var dish = _dishRepository.GetAllIncluding(o => o.Dish_FoodIngredient_Mapping).FirstOrDefault(o => o.Id == dishId);
            //var fip = _dish_FoodIgredient_MappingRepository.GetAllIncluding(o => o.FoodIgredient).Where(o => o.DishId == dishId);
            //dish.Cost = fip.Sum(o => o.FoodIgredient.Quantity > 0 ? o.FoodIgredient.Price / o.FoodIgredient.Quantity * o.Quantity : 0);
            //_dishRepository.Update(dish);
            return dish;
        }
    }
}
