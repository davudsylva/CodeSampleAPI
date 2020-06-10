using System;
using Xunit;
using Moq;
using ProductMicroservice.Core.Services;
using ProductMicroservice.Data.Repositories;
using System.Collections.Generic;
using ProductMicroservice.Contracts.Models;
using System.Threading.Tasks;

namespace ProductMicroservice.UnitTests
{
    public class ProductGetTests
    {
        // Given a set of existing products
        // When the complete product list is requested
        // Then a non-empty list should be returned
        [Fact]
        public async Task CanGetAllWithData()
        {
            var productService = EstablishEnvironment(withProduct: true);

            var result = await productService.GetByName(null);

            Assert.NotEmpty(result);
        }

        // Given a empty set of existing products
        // When the complete product list is requested
        // Then an empty list should be returned
        [Fact]
        public async Task CanGetAllWithNoData()
        {
            var productService = EstablishEnvironment(withProduct: false);

            var result = await productService.GetByName(null);

            Assert.Empty(result);
        }


        /* TODO:
         * Add a lot more unit tests here to make sure our validator works
         */

        protected ProductService EstablishEnvironment(bool withProduct)
        {
            // Establish the environment

            var productRepository = new Mock<IProductRepository>();
            var productOptionRepository = new Mock<IProductOptionRepository>();

            var testProduct = new Product() { Id = Guid.NewGuid(), Name = "testName", Description = "testDescription", Price = 2.5M, DeliveryPrice = 1.2M };

            if (withProduct)
            {
                productRepository.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync(new List<Product>() { testProduct });
                productRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Product>() { testProduct });
            }
            else
            {
                productRepository.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync(new List<Product>() );
                productRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Product>() { });
            }
            var productService = new ProductService(new ProductValidator(),
                                                    productRepository.Object,
                                                    productOptionRepository.Object);

            return productService;
        }

    }
}
