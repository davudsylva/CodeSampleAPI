using Refactored.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refactored.API.Models
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
