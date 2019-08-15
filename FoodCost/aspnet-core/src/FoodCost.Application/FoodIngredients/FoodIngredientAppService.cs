using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using FoodCost.FoodIngredients.Dto;
using FoodCost.Models.FoodIngredients;
using FoodCost.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCost.FoodIngredients
{
    [AbpAuthorize]
    public class FoodIngredientAppService : AsyncCrudAppService<FoodIngredient, FoodIngredientDto>
    {
        private FoodIngredientService _foodIngredientService;
        public FoodIngredientAppService(IRepository<FoodIngredient, int> repository, FoodIngredientService foodIngredientService) : base(repository)
        {
            _foodIngredientService = foodIngredientService;
        }

        public override Task<FoodIngredientDto> Get(EntityDto<int> input)
        {
            var fi = Repository.GetAll()
                .Include(o => o.FoodIngredient_Product_Mapping)
                    .ThenInclude(o => o.Product)
                    .ThenInclude(o => o.UnitOfMeasure)
                .Include(o => o.FoodIngredient_Product_Mapping)
                    .ThenInclude(o => o.UnitOfMeasure)
                .FirstOrDefault(o => o.Id == input.Id);

            var result = fi.MapTo<FoodIngredientDto>();

            result.Cost = fi.FoodIngredient_Product_Mapping.Sum(o => CalculateCost(o));

            return Task.FromResult(result);
        }

        public IList<FoodIngredient_ProductDto> GetProducts(int foodIngredientId)
        {
            var fi = Repository.GetAll()
                .Include(o => o.FoodIngredient_Product_Mapping)
                    .ThenInclude(o => o.Product)
                    .ThenInclude(o => o.UnitOfMeasure)
                .Include(o => o.FoodIngredient_Product_Mapping)
                    .ThenInclude(o => o.UnitOfMeasure)
                .FirstOrDefault(o => o.Id == foodIngredientId);
            return fi.FoodIngredient_Product_Mapping.Select(o =>
            {
                var fip = o.MapTo<FoodIngredient_ProductDto>();
                fip.Cost = CalculateCost(o);
                return fip;
            }).ToList();
        }

        private static decimal CalculateCost(FoodIngredient_Product o)
        {
            return (o.Quantity * o.UnitOfMeasure.BaseEquivalent) * (o.Product.Price / o.Product.UnitOfMeasure.BaseEquivalent);
        }

        public FoodIngredient_ProductDto AddProduct(int foodIngredientId, FoodIngredient_ProductDto foodIngredientProduct)
        {

            var fi = Repository.GetAllIncluding(
                o => o.FoodIngredient_Product_Mapping)
                .FirstOrDefault(o => o.Id == foodIngredientId);
            fi.FoodIngredient_Product_Mapping.Add(new FoodIngredient_Product
            {
                ProductId = foodIngredientProduct.ProductId,
                Quantity = foodIngredientProduct.Quantity,
                UnitOfMeasureId = foodIngredientProduct.UnitOfMeasureId
            });
            //_foodIngredientService.CalculateFoodIngredient(fi.Id);
            return foodIngredientProduct;
        }

        public void AddProducts(int foodIngredientId, List<FoodIngredient_ProductDto> foodIngredientProducts)
        {

            foreach (var fip in foodIngredientProducts)
            {
                AddProduct(foodIngredientId, fip);
            }
        }

        public void RemoveProductByProductId(int foodIngredientId, int productId)
        {
            var fi = Repository.GetAllIncluding(
                o => o.FoodIngredient_Product_Mapping).FirstOrDefault(o => o.Id == foodIngredientId);
            foreach (var fipm in fi.FoodIngredient_Product_Mapping.Where(o => o.ProductId == productId).ToList())
                fi.FoodIngredient_Product_Mapping.Remove(fipm);
            _foodIngredientService.CalculateFoodIngredient(fi.Id);
        }


        public void RemoveProduct(int foodIngredientId, int id)
        {
            var fi = Repository.GetAllIncluding(
                o => o.FoodIngredient_Product_Mapping).FirstOrDefault(o => o.Id == foodIngredientId);
            foreach (var fipm in fi.FoodIngredient_Product_Mapping.Where(o => o.Id == id).ToList())
                fi.FoodIngredient_Product_Mapping.Remove(fipm);
            _foodIngredientService.CalculateFoodIngredient(fi.Id);
        }


    }
}
