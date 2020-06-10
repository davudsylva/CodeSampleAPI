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
    public class ProductOptionTests : TestBase
    {
        [Fact]
        // Given a newly created product option
        // When I get that product
        // Then I should be able to get the product back
        public async Task CanCreateAndGetProductOption()
        {
            var controller = EstablishEnvironment();

            var testProduct = new Product 
            { 
                Name = Guid.NewGuid().ToString(), 
                Description = "Bob", 
                Price = 2.5M,
                DeliveryPrice = 1.5M
            };

            var testOption = new ProductOption
            {
                Name = "testOptionName",
                Description = "testOptionDescription"
            };

            // Ensure we can create a new product and option
            var createResponse = await controller.CreateProduct(testProduct);
            var okCreateResult = createResponse as OkObjectResult;
            Assert.NotNull(okCreateResult);
            var createdProduct = okCreateResult.Value as Product;

            var createOptionResponse = await controller.CreateOption(createdProduct.Id, testOption);
            var okCreateOptionResult = createOptionResponse as OkObjectResult;
            Assert.NotNull(okCreateOptionResult);
            var createdOption = okCreateOptionResult.Value as ProductOption;

            Assert.Equal(testOption.ProductId, createdProduct.Id);
            Assert.Equal(testOption.Name, createdOption.Name);
            Assert.Equal(testOption.Description, createdOption.Description);

            // Ensure we can get the product we just created
            var reGetResponse = await controller.GetOptionById(testOption.ProductId, testOption.Id);
            var reGetResult = reGetResponse as OkObjectResult;
            Assert.NotNull(reGetResult);
            var regotItem = reGetResult.Value as ProductOption;

            Assert.Equal(createdOption.Id, regotItem.Id);
            Assert.Equal(createdOption.ProductId, regotItem.ProductId);
            Assert.Equal(createdOption.Name, regotItem.Name);
            Assert.Equal(createdOption.Description, regotItem.Description);

            // Ensure we can get it from the product's options 
            // Ensure we can create a new product and option
            var listResponse = await controller.GetOptions(createdProduct.Id);
            var okListResult = listResponse as OkObjectResult;
            Assert.NotNull(okListResult);
            var createdProductOptions = okListResult.Value as ProductOptions;

            Assert.Equal(createdOption.Id, createdProductOptions.Items.First().Id);
            Assert.Equal(createdOption.ProductId, regotItem.ProductId);
            Assert.Equal(createdOption.Name, regotItem.Name);
            Assert.Equal(createdOption.Description, regotItem.Description);

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

            var testOption = new ProductOption
            {
                Name = "testOptionName",
                Description = "testOptionDescription"
            };


            // Ensure we can create a new product and option
            var createResponse = await controller.CreateProduct(testProduct);
            var okCreateResult = createResponse as OkObjectResult;
            Assert.NotNull(okCreateResult);
            var createdProduct = okCreateResult.Value as Product;

            var createOptionResponse = await controller.CreateOption(createdProduct.Id, testOption);
            var okCreateOptionResult = createOptionResponse as OkObjectResult;
            Assert.NotNull(okCreateOptionResult);
            var createdOption = okCreateOptionResult.Value as ProductOption;

            // Ensure we can get the product we just created
            var deleteResponse = await controller.DeleteOption(createdProduct.Id, createdOption.Id);
            var deleteResult = deleteResponse as OkResult;
            Assert.NotNull(deleteResult);

            // Ensure we can not get the product we just deleted
            var reGetResponse = await controller.GetOptionById(createdProduct.Id, createdOption.Id);
            var reGetResult = reGetResponse as NotFoundResult;
            Assert.NotNull(reGetResult);
        }


        [Fact]
        // Given a newly created product
        // When I delete that product
        // Then I should not be able to get it again
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

            var testOption = new ProductOption
            {
                Name = "testOptionName",
                Description = "testOptionDescription"
            };


            // Ensure we can create a new product and option
            var createResponse = await controller.CreateProduct(testProduct);
            var okCreateResult = createResponse as OkObjectResult;
            Assert.NotNull(okCreateResult);
            var createdProduct = okCreateResult.Value as Product;

            var createOptionResponse = await controller.CreateOption(createdProduct.Id, testOption);
            var okCreateOptionResult = createOptionResponse as OkObjectResult;
            Assert.NotNull(okCreateOptionResult);
            var createdOption = okCreateOptionResult.Value as ProductOption;

            // Ensure we can get the product we just created
            var reGetResponse = await controller.GetOptionById(createdProduct.Id, createdOption.Id);
            var reGetResult = reGetResponse as OkObjectResult;
            Assert.NotNull(reGetResult);
            var regotItem = reGetResult.Value as ProductOption;

            // Update the item
            regotItem.Name = "newName";
            regotItem.Description = "newDescription";
            var updateOptionResponse = await controller.UpdateOption(createdProduct.Id, createdOption.Id, regotItem);
            var updateResult = updateOptionResponse as OkResult;
            Assert.NotNull(updateResult);

            // Ensure the updates worked
            var reReGetResponse = await controller.GetOptionById(createdProduct.Id, createdOption.Id);
            var reReGetResult = reReGetResponse as OkObjectResult;
            Assert.NotNull(reReGetResult);
            var reReGotItem = reReGetResult.Value as ProductOption;

            Assert.Equal("newName", reReGotItem.Name);
            Assert.Equal("newDescription", reReGotItem.Description);
        }


        /* TODO:
         * 
         * More tests here will be required to test the end-to-end functionality
         */
    }
}
