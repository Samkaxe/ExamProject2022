using Application.DTOs;
using Core.Entities;

namespace Application.Interfaces;

public interface IBrandService
{
    Task<IReadOnlyList<ProductBrand>> GetAllTypes();
    
    Task<ProductBrand> GetProductBrandById(int id);
}