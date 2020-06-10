﻿using ProductMicroservice.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
