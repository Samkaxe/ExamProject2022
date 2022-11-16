using Application.Models.Base;

namespace Application.Models;

public class ProductModel: BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }
    public ProductTypeModel ProductType { get; set; }
    
    public int ProductTypeId { get; set; }
    public ProductBrandModel ProductBrand { get; set; }
    public int ProductBrandId { get; set; }
}