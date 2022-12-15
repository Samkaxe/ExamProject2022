using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Application.Models;

public class UploadRequest
{
    public IFormFile Image { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string? ImagePath { get; set; }
}