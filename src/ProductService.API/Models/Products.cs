using ProductMicroservice.Contracts.Models;
using System.Collections.Generic;

namespace ProductMicroservice.API.Models
{
    public class Products
    {
        public IEnumerable<Product> Items { get; private set; }

        public Products(IEnumerable<Product> items)
        {
            Items = items;
        }
    }
}
