using Xunit;
using System.Linq;
using System.Collections.Generic;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Refactored.Core.Services;
using Refactored.Data.Repositories;
using Microsoft.Extensions.Configuration;
using RefactoredThat.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refactored.Contracts.Models;
using System;

namespace Refactored.IntegrationTests
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

            Refactored.API.Mappers.DapperMappers.Config();

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
