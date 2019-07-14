using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Web.Models;
using FoodCost.Models.Products;
using FoodCost.Products.Dto;
using FoodCost.Services;
using System.Collections.Generic;
using System.Linq;

namespace FoodCost.Products
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

        public void RemoveProduct(int foodIngredientId, int productId)
        {
            var fi = Repository.GetAllIncluding(
                o => o.FoodIngredient_Product_Mapping).FirstOrDefault(o => o.Id == foodIngredientId);
            foreach (var fipm in fi.FoodIngredient_Product_Mapping.Where(o => o.ProductId == productId).ToList())
                fi.FoodIngredient_Product_Mapping.Remove(fipm);
            _foodIngredientService.CalculateFoodIngredient(fi.Id);
        }

    }
}
