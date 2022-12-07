using Core.Entities;

namespace Core.Interfaces;

public interface IBrandRepository
{
    Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
    
    Task<ProductBrand> GetProductBrandByIdAsync(int id);

    ProductBrand CreateBrand(ProductBrand brand);
    
    ProductBrand DeleteBrand(int id);
}