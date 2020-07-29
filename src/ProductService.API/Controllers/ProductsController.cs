using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductMicroservice.API.Models;
using ProductMicroservice.Contracts.Models;
using ProductMicroservice.Core.Services;

namespace ProductMicroservice.Controllers
{
    /// <summary>
    /// Operations to manage products andthier associated options.
    /// </summary>
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductsController : Controller
    {

        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService,  ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Returns the complete set of all products.
        /// </summary>
        /// <remarks>
        /// <para>Returns the complete set of all products optionally filtered by name.
        /// </para>
        /// </remarks>
        /// <returns>The set of filtered products</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts([FromQuery] string name)
        {
            var products = await _productService.GetByName(name);
            return Ok(new Products(products));
        }

        /// <summary>
        /// Returns the requested products.
        /// </summary>
        /// <remarks>
        /// <para>Returns the products specified by ID.
        /// </para>
        /// </remarks>
        /// <returns>The set of requested product</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new product, allocating an ID in the process.
        /// </para>
        /// </remarks>
        /// <returns>Returns newly created product, including its ID.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var newProduct = await _productService.Create(product);
            return Ok(newProduct);
        }

        /// <summary>
        /// Updates an new product.
        /// </summary>
        /// <remarks>
        /// <para>Updates the mutable attributes of the supplied product.
        /// </para>
        /// </remarks>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(Guid id, Product product)
        {
            var existingProduct = await _productService.GetById(product.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            await _productService.Update(product);
            return Ok();
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <remarks>
        /// <para>Deletes the specified product.
        /// </para>
        /// </remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var existingProduct = await _productService.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            await _productService.DeleteById(id);
            return Ok();
        }

        /// <summary>
        /// Gets the product options.
        /// </summary>
        /// <remarks>
        /// <para>Gets the options for the specified product. 
        /// </para>
        /// </remarks>
        /// <returns>The set product options</returns>
        [HttpGet("{productId}/options")]
        [ProducesResponseType(typeof(IEnumerable<ProductOption>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOptions(Guid productId)
        {
            var existingProduct = await _productService.GetById(productId);
            if (existingProduct == null)
            {
                return NotFound();
            }
            var productOptions = await _productService.GetProductOptions(productId);
            return Ok(new ProductOptions(productOptions));
        }

        /// <summary>
        /// Gets a specific product options.
        /// </summary>
        /// <remarks>
        /// <para>Gets the specified option from the specified product. 
        /// </para>
        /// </remarks>
        /// <returns>The requested product option</returns>
        [HttpGet("{productId}/options/{optionId}")]
        [ProducesResponseType(typeof(ProductOption), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOptionById(Guid productId, Guid optionId)
        {
            var option = await _productService.GetProductOptionById(productId, optionId);
            if (option == null)
            {
                return NotFound();
            }
            return Ok(option);
        }

        /// <summary>
        /// Adds an option to the product.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new option and adds it to the specified option.
        /// </para>
        /// </remarks>
        /// <returns>The newly created option</returns>
        [HttpPost("{productId}/options")]
        [ProducesResponseType(typeof(ProductOption), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateOption(Guid productId, ProductOption option)
        {
            var existingProduct = await _productService.GetById(productId);
            if (existingProduct == null)
            {
                return NotFound();
            }
            var newOption = await _productService.CreateOption(productId, option);
            return Ok(newOption);        
        }


        /// <summary>
        /// Updates an existing option
        /// </summary>
        /// <remarks>
        /// <para>Updates an existing option in an existing product.
        /// </para>
        /// </remarks>
        [HttpPut("{productId}/options/{optionId}")]
        [ProducesResponseType(typeof(ProductOption), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOption(Guid productId, Guid optionId, [FromBody]ProductOption option)
        {
            var existingOption = await _productService.GetProductOptionById(productId, optionId);
            if (existingOption == null)
            {
                return NotFound();
            }
            await _productService.UpdateOption(option);
            return Ok();
        }

        /// <summary>
        /// Deletes an existing option
        /// </summary>
        /// <remarks>
        /// <para>Deletes an existing option in an existing product.
        /// </para>
        /// </remarks>
        [HttpDelete("{productId}/options/{id}")]
        [ProducesResponseType(typeof(ProductOption), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOption(Guid productId, Guid optionId)
        {
            var existingOption = await _productService.GetProductOptionById(productId, optionId);
            if (existingOption == null)
            {
                return NotFound();
            }
            await _productService.DeleteOption(optionId);
            return Ok();
        }
    }
}
