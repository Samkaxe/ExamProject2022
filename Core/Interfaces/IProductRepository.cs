using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);

    Task<IReadOnlyList<Product>> GetProductsAsync();
    
    Product CreateNewProduct(Product product);
    
    Product UpdateProduct(Product product);
    
    Product DeleteProduct(int id);
}