using ProductMicroservice.Contracts.Models;
using System.Collections.Generic;

namespace ProductMicroservice.API.Models
{
    public class ProductOptions
    {
        public IEnumerable<ProductOption> Items { get; private set; }

        public ProductOptions(IEnumerable<ProductOption> items)
        {
            Items = items;
        }
    }
}
