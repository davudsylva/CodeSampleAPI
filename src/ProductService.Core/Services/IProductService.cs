using ProductMicroservice.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductMicroservice.Core.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetByName(string name);
        Task<Product> GetById(Guid productId);
        Task Update(Product product);
        Task<Product> Create(Product product);
        Task DeleteById(Guid id);
        Task<IEnumerable<ProductOption>> GetProductOptions(Guid productId);
        Task<ProductOption> GetProductOptionById(Guid productId, Guid optionId);
        Task<ProductOption> CreateOption(Guid productId, ProductOption productOption);
        Task UpdateOption(ProductOption productOption);
        Task DeleteOption(Guid optionId);
    }
}
