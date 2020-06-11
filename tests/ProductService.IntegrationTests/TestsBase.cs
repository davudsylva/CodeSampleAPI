using System.Collections.Generic;
using Moq;
using Microsoft.Extensions.Logging;
using ProductMicroservice.Core.Services;
using ProductMicroservice.Data.Repositories;
using Microsoft.Extensions.Configuration;
using ProductMicroservice.Controllers;

namespace ProductMicroservice.IntegrationTests
{
    public class TestBase
    {
        protected ProductsController EstablishEnvironment()
        {
            // Establish the environment
            var baseDir = System.IO.Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
               .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddInMemoryCollection(new[] { new KeyValuePair<string, string>("ConnectionString", "Data Source=../../../TestDatabase/products.db") })
              .Build();

            ProductMicroservice.API.Mappers.DapperMappers.Config();

            var productRepository = new ProductRepository(configuration);
            var productOptionRepository = new ProductOptionRepository(configuration);
            var productService = new ProductService(new ProductValidator(), 
                                                    productRepository, 
                                                    productOptionRepository);

            var logger = new Mock<ILogger<ProductsController>>();

            var controller = new ProductsController(productService, logger.Object);
            return controller;
        }
    }
}
