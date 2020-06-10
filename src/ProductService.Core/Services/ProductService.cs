using ProductMicroservice.Contracts.Models;
using ProductMicroservice.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.Core.Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IProductOptionRepository _productOptionRepository;
        IProductValidator _productValidator;

        public ProductService(IProductValidator productValidator, IProductRepository productRepository, IProductOptionRepository productOptionRepository)
        {
            _productRepository = productRepository;
            _productOptionRepository = productOptionRepository;
            _productValidator = productValidator;
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _productRepository.GetAll();
            }
            else
            {
                return await _productRepository.GetByName(name);
            }
        }

        public async Task<Product> GetById(Guid productId)
        {
            return await _productRepository.GetById(productId);
        }

        public async Task Update(Product product)
        {
            await _productRepository.Update(product);
        }

        public async Task<Product> Create(Product product)
        {
            var validation = _productValidator.ValidateProduct(product);
            if (!validation.isOk)
            {
                throw new Exception($"Validation Error: {validation.reason}");
            }
            product.Id = Guid.NewGuid();
            await _productRepository.Create(product);
            return product;
        }

        public async Task DeleteById(Guid productId)
        {
            await _productRepository.DeleteById(productId);
        }

        public async Task<IEnumerable<ProductOption>> GetProductOptions(Guid productId)
        {
            return await _productOptionRepository.GetProductOptions(productId);
        }

        public async Task<ProductOption> GetProductOptionById(Guid productId, Guid optionId)
        {
            return await _productOptionRepository.GetProductOptionById(productId, optionId);
        }

        public async Task<ProductOption> CreateOption(Guid productId, ProductOption productOption)
        {
            productOption.ProductId = productId;
            productOption.Id = Guid.NewGuid();

            var validation = _productValidator.ValidateProductOption(productOption);
            if (!validation.isOk)
            {
                throw new Exception($"Validation Error: {validation.reason}");
            }

            await _productOptionRepository.CreateOption(productOption);
            return productOption;
        }

        public async Task UpdateOption(ProductOption productOption)
        {
            await _productOptionRepository.UpdateOption(productOption);
        }

        public async Task DeleteOption(Guid optionId)
        {
            await _productOptionRepository.DeleteOption(optionId);
        }
    }
}
