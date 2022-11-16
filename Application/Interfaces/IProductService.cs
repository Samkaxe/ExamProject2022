using Application.DTOs;
using Application.Models;
using Core.Entities;

namespace Application.Interfaces;

public interface IProductService
{
     Task<IReadOnlyList<ProductModel>> GetAllProducts();
     Product CreateNewProduct(ProductToCreateDTO dto);
     Task<Product> GetProductById(int id);
     Product UpdateProduct(int id, Product product);
     Product DeleteProduct(int id);
}