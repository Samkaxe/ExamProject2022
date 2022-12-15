using System.Text.Json.Serialization;

namespace API.Models;

public class UploadResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public bool Success { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string ErrorCode { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string Error { get; set; }
    public string ImagePath { get; set; }
}