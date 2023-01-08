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
            .Include(p => p.ProductBrand)//Specifies related entities to include in the query results.
            .FirstOrDefaultAsync(p => p.Id == id);
          //Asynchronously returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .ToListAsync();//Asynchronously creates a List<T> from an IQueryable<out T> by enumerating it asynchronously.
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