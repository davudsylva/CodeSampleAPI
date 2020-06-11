using ProductMicroservice.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductMicroservice.Data.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetByName(string name);
        Task<Product> GetById(Guid productId);
        Task Update(Product product);
        Task Create(Product product);
        Task DeleteById(Guid productId);
    }
}
