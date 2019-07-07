using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using FoodCost.Dishes.Dto;
using FoodCost.Models.Dishes;
using FoodCost.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodCost.Dishes
{
    public class DishAppService: AsyncCrudAppService<Dish, DishDto>
    {
        private DishService _dishService;
        public DishAppService(IRepository<Dish, int> repository, DishService dishService) : base(repository)
        {
            _dishService = dishService;
        }

        public IList<Dish_FoodIngredientDto> GetFoodIngredients(int dishId)
        {
            var fi = Repository.GetAllIncluding(o => o.Dish_FoodIngredient_Mapping).FirstOrDefault(o => o.Id == dishId);
            return fi.Dish_FoodIngredient_Mapping.Select(o => o.MapTo<Dish_FoodIngredientDto>()).ToList();
        }

        public Dish_FoodIngredientDto AddFoodIngredient(int dishId, Dish_FoodIngredientDto dishFoodIngredient)
        {
            var fi = Repository.GetAllIncluding(
                o => o.Dish_FoodIngredient_Mapping)
                .FirstOrDefault(o => o.Id == dishId);
            fi.Dish_FoodIngredient_Mapping.Add(new Dish_FoodIngredient
            {
                FoodIngredientId = dishFoodIngredient.FoodIngredientId,
                Quantity = dishFoodIngredient.Quantity,
                UnitOfMeasureId = dishFoodIngredient.UnitOfMeasureId
            });
            _dishService.CalculateDish(fi.Id);
            return dishFoodIngredient;
        }

        public void RemoveFoodIngredient(int dishId, int foodIngredientId)
        {
            var fi = Repository.GetAllIncluding(
                o => o.Dish_FoodIngredient_Mapping).FirstOrDefault(o => o.Id == dishId);
            foreach (var fipm in fi.Dish_FoodIngredient_Mapping.Where(o => o.FoodIngredientId == foodIngredientId).ToList())
                fi.Dish_FoodIngredient_Mapping.Remove(fipm);
            _dishService.CalculateDish(fi.Id);
        }
    }
}
