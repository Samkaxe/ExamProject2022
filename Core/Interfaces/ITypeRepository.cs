using Core.Entities;

namespace Core.Interfaces;

public interface ITypeRepository
{
    Task<IReadOnlyList<ProductType>> GetTypesAsync();
    
    Task<ProductType> GetProductTypeByIdAsync(int id);

    ProductType CreateType(ProductType type);

    ProductType DeleteType(int id);
}