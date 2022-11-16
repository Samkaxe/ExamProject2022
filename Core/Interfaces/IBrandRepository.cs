using Core.Entities;

namespace Core.Interfaces;

public interface IBrandRepository
{
    Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
    
    Task<ProductBrand> GetProductBrandByIdAsync(int id);
}