using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;

    public ProductRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .ToListAsync();
    }

    public Product CreateNewProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public Product UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
        return product;
    }

    public Product DeleteProduct(int id)
    {
        var productToDelete = _context.Products.Find(id) ?? throw new KeyNotFoundException();
        _context.Products.Remove(productToDelete);
        _context.SaveChanges();
        return productToDelete;
    }
}