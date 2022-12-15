using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class ProductBrandToCreateDTO
{
    [Required]
    public string Name { get; set; }
}