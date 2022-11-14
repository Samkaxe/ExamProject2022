using Core.Entities;

namespace Core.Interfaces;

public interface ITypeRepository
{
    Task<IReadOnlyList<ProductType>> GetTypesAsync();
}