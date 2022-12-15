using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class BrandRepository : IBrandRepository
{
    private readonly StoreContext _context;

    public BrandRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
    {
        return await _context.ProductBrands.ToListAsync();
    }

    public async Task<ProductBrand> GetProductBrandByIdAsync(int id)
    {
       return await _context.ProductBrands.FirstOrDefaultAsync(p => p.Id == id);
    }

    public ProductBrand CreateBrand(ProductBrand brand)
    {
        _context.ProductBrands.Add(brand);
        _context.SaveChanges();
        return brand;
    }

    public ProductBrand DeleteBrand(int id)
    {
        var brand = _context.ProductBrands.Find(id) ?? throw new KeyNotFoundException();
        _context.ProductBrands.Remove(brand);
        _context.SaveChanges();
        return brand;
    }
}