using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using FoodCost.Dishes.Dto;
using FoodCost.Models.Dishes;
using FoodCost.Models.FoodIngredients;
using FoodCost.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodCost.Dishes
{
    public class DishAppService : AsyncCrudAppService<Dish, DishDto>
    {
        private DishService _dishService;
        private IRepository<Dish_FoodIngredient> _dish_FoodIngredientRepository;
        public DishAppService(IRepository<Dish, int> repository,
            DishService dishService,
            IRepository<Dish_FoodIngredient> dish_FoodIngredientRepository) : base(repository)
        {
            _dishService = dishService;
            _dish_FoodIngredientRepository = dish_FoodIngredientRepository;
        }

        public IList<Dish_FoodIngredientDto> GetFoodIngredients(int dishId)
        {
            var dishFoodIngredients = _dish_FoodIngredientRepository.GetAll()
                .Include(o => o.FoodIngredient)
                    .ThenInclude(o => o.FoodIngredient_Product_Mapping)
                    .ThenInclude(o => o.Product)
                    .ThenInclude(o => o.UnitOfMeasure)
                .Include(o => o.FoodIngredient)
                    .ThenInclude(o => o.FoodIngredient_Product_Mapping)
                    .ThenInclude(o => o.UnitOfMeasure)
                .Where(o => o.DishId == dishId)
                .ToList();

            var result = dishFoodIngredients.Select(o =>
            {
                var dfi = o.MapTo<Dish_FoodIngredientDto>();
                dfi.Cost = o.FoodIngredient.FoodIngredient_Product_Mapping.Sum(x => CalculateCost(x));
                return dfi;
            })
            .ToList();


            return result;



        }

        private static decimal CalculateCost(FoodIngredient_Product o)
        {
            return (o.Quantity * o.UnitOfMeasure.BaseEquivalent) * (o.Product.Price / o.Product.UnitOfMeasure.BaseEquivalent);
        }

        public Dish_FoodIngredientDto AddFoodIngredient(int dishId, Dish_FoodIngredientDto dishFoodIngredient)
        {
            var dish = Repository.GetAllIncluding(
                o => o.Dish_FoodIngredient_Mapping)
                .FirstOrDefault(o => o.Id == dishId);
            dish.Dish_FoodIngredient_Mapping.Add(new Dish_FoodIngredient
            {
                FoodIngredientId = dishFoodIngredient.FoodIngredientId,
                Quantity = dishFoodIngredient.Quantity,
                UnitOfMeasureId = dishFoodIngredient.UnitOfMeasureId
            });
            //_dishService.CalculateDish(fi.Id);

            Repository.Update(dish);
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
