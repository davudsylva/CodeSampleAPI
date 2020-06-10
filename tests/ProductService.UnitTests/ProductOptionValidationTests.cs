using System;
using Xunit;
using ProductMicroservice.Core.Services;

namespace ProductMicroservice.UnitTests
{
    public class ProductOptionValidationTests
    {
        // Given a product option with a null description
        // When the product is validated
        // Then the a failure status should be returned
        [Fact]
        public void CanValidateNullDescription()
        {
            var validator = new ProductValidator();

            var result = validator.ValidateProductOption(new Contracts.Models.ProductOption() { Description = null });

            Assert.False(result.isOk);
        }

        // Given a product option with a valid description
        // When the product is validated
        // Then the a failure status should be returned
        [Fact]
        public void CanValidateValidDescription()
        {
            var validator = new ProductValidator();

            var result = validator.ValidateProductOption(new Contracts.Models.ProductOption() { Description = "Banana", Name="B1" });

            Assert.True(result.isOk);
        }

        /* TODO:
         * Add a lot more unit tests here to make sure our validator works
         */
    }
}
