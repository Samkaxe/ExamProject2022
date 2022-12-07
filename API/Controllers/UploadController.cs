using API.Models;
using Application.Interfaces;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UploadController : BaseApiController
{
    private readonly IUploadService _uploadService;
    private readonly IWebHostEnvironment _environment;

    public UploadController(IUploadService uploadService, IWebHostEnvironment environment)
    {
        _uploadService = uploadService;
        _environment = environment;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitPost([FromForm] UploadRequest request)
    {
        if (request == null)
            return BadRequest(new UploadResponse {Success = false, ErrorCode = "01", Error = "Invalid post request"});
        if (string.IsNullOrEmpty(Request.GetMultipartBoundary()))
            return BadRequest(new UploadResponse {Success = false, ErrorCode = "02", Error = "Invalid post header"});
        if (request.Image != null) await _uploadService.SavePostImageAsync(request,_environment.WebRootPath);
        var response = await _uploadService.CreatePostAsync(request);
        
        return Ok(response);

    }
}