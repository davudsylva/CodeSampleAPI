using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductMicroservice.Contracts.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
