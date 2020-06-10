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
using Refactored.API.Models;

namespace Refactored.IntegrationTests
{
    // Integration tests design to test round trip to a live database. 
    public class ProductTests : TestBase
    {
        [Fact]
        // Given a newly created product
        // When I get that product
        // Then I should be able to get the product back
        public async Task CanCreateAndGetProduct()
        {
            var controller = EstablishEnvironment();

            var testProduct = new Product 
                                { Name = Guid.NewGuid().ToString(), 
                                  Description = "Bob", 
                                  Price = 2.5M,
                                  DeliveryPrice = 1.5M
            };

            // Ensure we can create a new product
            var createResponse = await controller.CreateProduct(testProduct);
            var okCreateResult = createResponse as OkObjectResult;
            Assert.NotNull(okCreateResult);
            var createdItem = okCreateResult.Value as Product;

            // Ensure we can get the product we just created
            var reGetResponse = await controller.GetProductById(createdItem.Id);
            var reGetResult = reGetResponse as OkObjectResult;
            Assert.NotNull(reGetResult);
            var regotItem = reGetResult.Value as Product;

            Assert.Equal(createdItem.Id, regotItem.Id);
            Assert.Equal(createdItem.Name, regotItem.Name);
            Assert.Equal(createdItem.Description, regotItem.Description);
            Assert.Equal(createdItem.DeliveryPrice, regotItem.DeliveryPrice);
        }

        [Fact]
        // Given a newly created product
        // When I get that product
        // Then I should be able to search for the product 
        public async Task CanCreateAndSearchProduct()
        {
            var controller = EstablishEnvironment();

            var testProduct = new Product
            {
                Name = Guid.NewGuid().ToString(),
                Description = "Bob",
                Price = 2.5M,
                DeliveryPrice = 1.5M
            };

            // Ensure we can create a new product
            var createResponse = await controller.CreateProduct(testProduct);
            var okCreateResult = createResponse as OkObjectResult;
            Assert.NotNull(okCreateResult);
            var createdItem = okCreateResult.Value as Product;

            // Ensure we can search for the product we just created
            var searchResponse = await controller.GetProducts(testProduct.Name);
            var searchResult = searchResponse as OkObjectResult;
            Assert.NotNull(searchResult);
            var searchItems = searchResult.Value as Products;

            Assert.Single(searchItems.Items);
            Assert.Equal(createdItem.Id, searchItems.Items.First().Id);
            Assert.Equal(createdItem.Name, searchItems.Items.First().Name);
            Assert.Equal(createdItem.Description, searchItems.Items.First().Description);
            Assert.Equal(createdItem.DeliveryPrice, searchItems.Items.First().DeliveryPrice);
        }


        [Fact]
        // Given a newly created product
        // When I delete that product
        // Then I should not be able to get it again
        public async Task CanCreateAndDeleteProduct()
        {
            var controller = EstablishEnvironment();

            var testProduct = new Product
            {
                Name = Guid.NewGuid().ToString(),
                Description = "Bob",
                Price = 2.5M,
                DeliveryPrice = 1.5M
            };

            // Ensure we can create a new product
            var createResponse = await controller.CreateProduct(testProduct);
            var okCreateResult = createResponse as OkObjectResult;
            Assert.NotNull(okCreateResult);
            var createdItem = okCreateResult.Value as Product;

            // Ensure we can get the product we just created
            var deleteResponse = await controller.DeleteProduct(createdItem.Id);
            var deleteResult = deleteResponse as OkResult;
            Assert.NotNull(deleteResult);

            // Ensure we can not get the product we just deleted
            var reGetResponse = await controller.GetProductById(createdItem.Id);
            var reGetResult = reGetResponse as NotFoundResult;
            Assert.NotNull(reGetResult);
        }


        [Fact]
        // Given a newly created product
        // When I update that product
        // Then I should be able to get it again and see the changes
        public async Task CanCreateAndUpdateProduct()
        {
            var controller = EstablishEnvironment();

            var testProduct = new Product
            {
                Name = Guid.NewGuid().ToString(),
                Description = "Bob",
                Price = 2.5M,
                DeliveryPrice = 1.5M
            };

            // Ensure we can create a new product
            var createResponse = await controller.CreateProduct(testProduct);
            var okCreateResult = createResponse as OkObjectResult;
            Assert.NotNull(okCreateResult);
            var createdItem = okCreateResult.Value as Product;

            createdItem.Name = "newName";
            createdItem.Description = "newDescription";

            // Ensure we can update the product we just created
            var updateResponse = await controller.UpdateProduct(createdItem.Id, createdItem);
            var updateResult = updateResponse as OkResult;
            Assert.NotNull(updateResult);

            // Ensure we can get the product we just updated
            var reGetResponse = await controller.GetProductById(createdItem.Id);
            var reGetResult = reGetResponse as OkObjectResult;
            Assert.NotNull(reGetResult);
            var reGotItem = reGetResult.Value as Product;

            Assert.Equal("newName", reGotItem.Name);
            Assert.Equal("newDescription", reGotItem.Description);
        }


        /* TODO:
         * 
         * More tests here will be required to test the end-to-end functionality
         */
    }
}
