using ProductMicroservice.Contracts.Models;
using ProductMicroservice.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.Core.Services
{
    public class ProductValidator : IProductValidator
    {

        public (bool isOk, string reason) ValidateProduct(Product product)
        {
            if (String.IsNullOrEmpty(product.Description))
            {
                return (false, "Empty description");
            }
            if (String.IsNullOrEmpty(product.Name))
            {
                return (false, "Empty name");
            }
            if (product.Price < 0)
            {
                return (false, "Invalid price");
            }
            if (product.DeliveryPrice < 0)
            {
                return (false, "Invalid delivery price");
            }
            // TODO: Add more checks here

            return (true, null);
        }


        public (bool isOk, string reason) ValidateProductOption(ProductOption option)
        {
            if (String.IsNullOrEmpty(option.Description))
            {
                return (false, "Empty description");
            }
            if (String.IsNullOrEmpty(option.Name))
            {
                return (false, "Empty name");
            }
            // TODO: Add more checks here

            return (true, null);
        }

    }
}
