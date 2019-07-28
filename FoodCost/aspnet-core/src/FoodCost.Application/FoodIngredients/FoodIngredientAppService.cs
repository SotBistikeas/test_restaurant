using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using FoodCost.FoodIngredients.Dto;
using FoodCost.Models.FoodIngredients;
using FoodCost.Services;
using System.Collections.Generic;
using System.Linq;

namespace FoodCost.FoodIngredients
{
    public class FoodIngredientAppService : AsyncCrudAppService<FoodIngredient, FoodIngredientDto>
    {
        private FoodIngredientService _foodIngredientService;
        public FoodIngredientAppService(IRepository<FoodIngredient, int> repository, FoodIngredientService foodIngredientService) : base(repository)
        {
            _foodIngredientService = foodIngredientService;
        }

        public IList<FoodIngredient_ProductDto> GetProducts(int foodIngredientId)
        {
            var fi = Repository.GetAllIncluding(o => o.FoodIngredient_Product_Mapping).FirstOrDefault(o => o.Id == foodIngredientId);
            return fi.FoodIngredient_Product_Mapping.Select(o => o.MapTo<FoodIngredient_ProductDto>()).ToList();
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
