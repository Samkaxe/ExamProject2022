using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class TypeRepository : ITypeRepository
{
    private readonly StoreContext _context;

    public TypeRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ProductType>> GetTypesAsync()
    {
        return await _context.ProductTypes.ToListAsync();
    }

    public async Task<ProductType> GetProductTypeByIdAsync(int id)
    {
        return await _context.ProductTypes.FirstOrDefaultAsync(p => p.Id == id);
    }

    public ProductType CreateType(ProductType type)
    {
        _context.ProductTypes.Add(type);
        _context.SaveChanges();
        return type;
    }

    public ProductType DeleteType(int id)
    {
        var type = _context.ProductTypes.Find(id) ?? throw new KeyNotFoundException();
        _context.ProductTypes.Remove(type);
        _context.SaveChanges();
        return type;
    }
}