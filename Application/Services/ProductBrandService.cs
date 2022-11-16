using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

public class ProductBrandService : IBrandService
{
    private readonly IBrandRepository _repo;

    public ProductBrandService(IBrandRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetAllTypes()
    {
        return await _repo.GetBrandsAsync();
    }

    public async Task<ProductBrand> GetProductBrandById(int id)
    {
        return await _repo.GetProductBrandByIdAsync(id);
    }
}