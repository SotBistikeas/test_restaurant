using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using FoodCost.Models.FoodIngredients;
using FoodCost.Models.Products;
using FoodCost.Products.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCost.Products
{
    public class ProductAppService : AsyncCrudAppService<Product, ProductDto>
    {
        private readonly IRepository<Product, int> _productRepository;
        private readonly IRepository<FoodIngredient> _foodIngredientRepository;

        public ProductAppService(IRepository<Product, int> repository,
            IRepository<FoodIngredient> foodIngredientRepository) : base(repository)
        {
            _productRepository = repository;
            _foodIngredientRepository = foodIngredientRepository;
        }

        public override async Task<ProductDto> Create(ProductDto input)
        {
            var product = input.MapTo<Product>();
            await _productRepository.InsertAndGetIdAsync(product);

            var foodIngredient = new FoodIngredient
            {
                Name = input.Name,
                UnitOfMeasureId = input.UnitOfMeasureId,
                IsProduct = true

            };
            foodIngredient.FoodIngredient_Product_Mapping.Add(new FoodIngredient_Product
            {
                ProductId = product.Id,
                Quantity = 1,
                UnitOfMeasureId = input.UnitOfMeasureId
            });


            await _foodIngredientRepository.InsertAndGetIdAsync(foodIngredient);

            return product.MapTo<ProductDto>();
        }

        public override async Task<ProductDto> Update(ProductDto input)
        {
            var product = input.MapTo<Product>();
            await _productRepository.UpdateAsync(product);

            //find corresponding food ingredient
            var foodIngredient = _foodIngredientRepository.GetAllIncluding(o => o.FoodIngredient_Product_Mapping)
                .Where(o => o.IsProduct)
                .Where(o => o.FoodIngredient_Product_Mapping.All(x => x.ProductId == input.Id))
                .FirstOrDefault();

            if (foodIngredient == null)
            {
                foodIngredient = new FoodIngredient
                {
                    Name = input.Name,
                    UnitOfMeasureId = input.UnitOfMeasureId,
                    IsProduct = true

                };
                foodIngredient.FoodIngredient_Product_Mapping.Add(new FoodIngredient_Product
                {
                    ProductId = product.Id,
                    Quantity = 1,
                    UnitOfMeasureId = input.UnitOfMeasureId
                });

                await _foodIngredientRepository.InsertAndGetIdAsync(foodIngredient);
            }
            else
            {
                foodIngredient.Name = input.Name;
                foodIngredient.UnitOfMeasureId = input.UnitOfMeasureId;
                var fipm = foodIngredient.FoodIngredient_Product_Mapping.FirstOrDefault();
                fipm.UnitOfMeasureId = input.UnitOfMeasureId;
            }

            await _foodIngredientRepository.InsertOrUpdateAndGetIdAsync(foodIngredient);


            return product.MapTo<ProductDto>();
        }

        public override Task Delete(EntityDto<int> input)
        {

            //find corresponding food ingredient
            var foodIngredient = _foodIngredientRepository.GetAllIncluding(o => o.FoodIngredient_Product_Mapping)
                .Where(o => o.IsProduct)
                .Where(o => o.FoodIngredient_Product_Mapping.All(x => x.ProductId == input.Id))
                .FirstOrDefault();

            if (foodIngredient != null)
            {
                _foodIngredientRepository.Delete(foodIngredient);
            }


            return base.Delete(input);
        }
    }
}
