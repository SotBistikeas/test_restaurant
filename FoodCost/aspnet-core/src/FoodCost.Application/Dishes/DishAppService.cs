﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using FoodCost.Dishes.Dto;
using FoodCost.Models.Dishes;
using FoodCost.Models.FoodIngredients;
using FoodCost.Models.RestaurantSettings;
using FoodCost.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCost.Dishes
{
    [AbpAuthorize]
    public class DishAppService : AsyncCrudAppService<Dish, DishDto>
    {
        private readonly DishService _dishService;
        private readonly IRepository<Dish_FoodIngredient> _dish_FoodIngredientRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<RestaurantSetting> _restaurantSettingsRepository;

        public DishAppService(IRepository<Dish, int> repository,
            DishService dishService,
            IRepository<Dish_FoodIngredient> dish_FoodIngredientRepository,
            IObjectMapper objectMapper,
            IRepository<RestaurantSetting> restaurantSettingsRepository) : base(repository)
        {
            _dishService = dishService;
            _dish_FoodIngredientRepository = dish_FoodIngredientRepository;
            _objectMapper = objectMapper;
            _restaurantSettingsRepository = restaurantSettingsRepository;
        }

        public async Task<DishFullDto> GetFull(EntityDto<int> input)
        {


            var dish = await Repository.GetAsync(input.Id);

            var setting = await _restaurantSettingsRepository.FirstOrDefaultAsync(o => o.TenantId == AbpSession.TenantId);
            decimal fixedCost = setting.ExtraCostPerServing;
            decimal saleFactor = setting.BaseFactor;
            decimal vat = 0.24m;


            var dishFull = _objectMapper.Map<DishFullDto>(dish);

            dishFull.FoodIngredients = GetFoodIngredients(input.Id);

            dishFull.BaseCost = dishFull.FoodIngredients.Sum(o => o.Cost);
            dishFull.RealCost = dishFull.BaseCost + fixedCost;

            dishFull.SalePriceExclTax = dishFull.BaseCost * saleFactor;
            dishFull.SalePriceInclTax = dishFull.SalePriceExclTax * (1.0m + vat);


            return dishFull;

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
                var dfi = _objectMapper.Map<Dish_FoodIngredientDto>(o);
                dfi.Cost = o.Quantity * o.FoodIngredient.FoodIngredient_Product_Mapping.Sum(x => CalculateCost(x));
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
            foreach (var fipm in fi.Dish_FoodIngredient_Mapping.Where(o => o.Id == foodIngredientId).ToList())
                fi.Dish_FoodIngredient_Mapping.Remove(fipm);

            _dishService.CalculateDish(fi.Id);
            Repository.Update(fi);
        }
    }
}
