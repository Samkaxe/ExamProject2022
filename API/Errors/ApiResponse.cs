namespace API.Errors;

public class ApiResponse
{
    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }
    
    public int StatusCode { get; set; }
    public string Message { get; set; }
    
    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "A bad request ",
            401 => "Authorized ",
            404 => "Resource found ",
            500 => " the server encountered an unexpected condition that prevented it from fulfilling the request",
            
            _ => null
        };
    }

}