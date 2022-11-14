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
}