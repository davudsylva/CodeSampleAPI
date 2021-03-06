﻿using ProductMicroservice.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductMicroservice.Data.Repositories
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
