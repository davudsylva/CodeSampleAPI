using ProductMicroservice.Contracts.Models;

namespace ProductMicroservice.Core.Services
{
    public interface IProductValidator
    {
        (bool isOk, string reason) ValidateProduct(Product product);
        (bool isOk, string reason) ValidateProductOption(ProductOption option);
    }
}
