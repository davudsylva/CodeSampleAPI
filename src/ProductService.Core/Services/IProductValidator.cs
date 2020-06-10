using Refactored.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Refactored.Core.Services
{
    public interface IProductValidator
    {
        (bool isOk, string reason) ValidateProduct(Product product);
        (bool isOk, string reason) ValidateProductOption(ProductOption option);
    }
}
