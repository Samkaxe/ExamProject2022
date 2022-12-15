using Application.DTOs;
using Core.Entities;

namespace Application.Interfaces;

public interface IBrandService
{
    Task<IReadOnlyList<ProductBrand>> GetAllBrands();
    
    Task<ProductBrand> GetProductBrandById(int id);
    
    ProductBrand CreateBrand(ProductBrandToCreateDTO dto);
    
    ProductBrand DeleteBrand(int id);
}