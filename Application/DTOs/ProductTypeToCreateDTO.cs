using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class ProductTypeToCreateDTO
{
    [Required]
    public string Name { get; set; }
}