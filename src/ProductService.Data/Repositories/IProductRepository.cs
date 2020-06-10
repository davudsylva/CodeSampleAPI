using Refactored.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Refactored.Data.Repositories
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
