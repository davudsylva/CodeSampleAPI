using System;
using Xunit;
using ProductMicroservice.Core.Services;

namespace ProductMicroservice.UnitTests
{
    public class ProductValidationTests
    {
        // Given a product with a null description
        // When the product is validated
        // Then the a failure status should be returned
        [Fact]
        public void CanValidateNullDescription()
        {
            var validator = new ProductValidator();

            var result = validator.ValidateProduct(new Contracts.Models.Product() { Description = null });

            Assert.False(result.isOk);
        }

        // Given a product with a valid description
        // When the product is validated
        // Then the a failure status should be returned
        [Fact]
        public void CanValidateValidDescription()
        {
            var validator = new ProductValidator();

            var result = validator.ValidateProduct(new Contracts.Models.Product() { Description = "Banana", Name="B1" });

            Assert.True(result.isOk);
        }

        // Given a product with a valid description
        // When the product is validated
        // Then the a failure status should be returned
        [Theory]
        [InlineData("-1.0", false)]
        [InlineData("0.0", false)]
        [InlineData("1.0", false)]
        public void CanValidatePrice(string testPrice, bool expectedValid)
        {
            var actualPrice = Decimal.Parse(testPrice);
            var validator = new ProductValidator();

            var result = validator.ValidateProduct(new Contracts.Models.Product() { Description = "Banana", Price= actualPrice, DeliveryPrice=10.9M });

            Assert.Equal(expectedValid, result.isOk);
        }

        // Given a product with a valid description
        // When the product is validated
        // Then the a failure status should be returned
        [Theory]
        [InlineData("-1.0", false)]
        [InlineData("0.0", false)]
        [InlineData("1.0", false)]
        public void CanValidateDeliveryPrice(string testPrice, bool expectedValid)
        {
            var actualPrice = Decimal.Parse(testPrice);
            var validator = new ProductValidator();

            var result = validator.ValidateProduct(new Contracts.Models.Product() { Description = "Banana", Price = 10.9M, DeliveryPrice = actualPrice });

            Assert.Equal(expectedValid, result.isOk);
        }


        /* TODO:
         * Add a lot more unit tests here to make sure our validator works
         */
    }
}
