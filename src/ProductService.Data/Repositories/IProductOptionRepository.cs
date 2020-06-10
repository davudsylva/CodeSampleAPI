using Refactored.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Refactored.Data.Repositories
{
    public interface IProductOptionRepository
    {
        Task<IEnumerable<ProductOption>> GetProductOptions(Guid productId);
        Task<ProductOption> GetProductOptionById(Guid productId, Guid optionId);
        Task CreateOption(ProductOption productOption);
        Task UpdateOption(ProductOption productOption);
        Task DeleteOption(Guid optionId);
    }
}
