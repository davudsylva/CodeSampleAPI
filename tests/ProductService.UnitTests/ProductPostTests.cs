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
    public class ProductPostTests
    {
        // Given an invalid product
        // When we try to save it
        // Then we expect an exception 
        //  and for the repository's crate meth to be not called.
        [Fact]
        public async Task CantCreateInvalidProduct()
        {
            var testEnvironment = EstablishEnvironment();

            var product = new Product()
            {
                Name = "Box",
                Description = "Cubic container",
                Price = -10.50M,
                DeliveryPrice = 5.50M,
            };

            await Assert.ThrowsAsync<Exception>(() => testEnvironment.productService.Create(product));

            testEnvironment.productRepositoryMock.Verify(x => x.Create(It.IsAny<Product>()), Times.Never);
        }

        // Given a valid product
        // When the we try and save it
        // Then we expect the repositry to be called to save it
        //  and a valid response to be returned
        [Fact]
        public async Task CantCreateValidProduct()
        {
            var testEnvironment = EstablishEnvironment();

            var product = new Product()
            {
                Name = "Box",
                Description = "Cubic container",
                Price = 10.50M,
                DeliveryPrice = 5.50M,
            };

            var result = await testEnvironment.productService.Create(product);

            Assert.NotNull(result);

            testEnvironment.productRepositoryMock.Verify(x => x.Create(It.IsAny<Product>()), Times.Once);
        }


        /* TODO:
         * Add a lot more unit tests here 
         */

        protected (ProductService productService, Mock<IProductRepository> productRepositoryMock) EstablishEnvironment()
        {
            // Establish the environment

            var productRepositoryMock = new Mock<IProductRepository>();
            var productOptionRepositoryMock = new Mock<IProductOptionRepository>();

            var testProduct = new Product() { Id = Guid.NewGuid(), Name = "testName", Description = "testDescription", Price = 2.5M, DeliveryPrice = 1.2M };

            productRepositoryMock.Setup(x => x.Create(It.IsAny<Product>()));

            var productService = new ProductService(new ProductValidator(),
                                                    productRepositoryMock.Object,
                                                    productOptionRepositoryMock.Object);

            return (productService, productRepositoryMock);
        }

    }
}
