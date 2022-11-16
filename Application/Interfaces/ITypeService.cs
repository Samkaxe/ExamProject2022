using Application.DTOs;
using Core.Entities;

namespace Application.Interfaces;

public interface ITypeService
{
    Task<IReadOnlyList<ProductType>> GetAllTypes();
    
    Task<ProductType> GetTypeById(int id);
      
}