using API.Models;
using Application.Helper;
using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public class UploadService : IUploadService
{
    public async Task SavePostImageAsync(UploadRequest uploadRequest, string environmentWebRootPath)
    {
        var filePath = GetFilePath(environmentWebRootPath, uploadRequest.Image.FileName);
        await uploadRequest.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
        uploadRequest.ImagePath = filePath;
    }

    

    public async Task<UploadResponse> CreatePostAsync(UploadRequest postRequest)
    {
        return new UploadResponse
            {Success = true, ImagePath = Path.Combine("images", "products", Path.GetFileName(postRequest.ImagePath))};
    }
    
    
    
    private static string GetFilePath(string environmentWebRootPath, string imageFileName)
    {
        var uniqueFileName = FileHelper.GetUniqueFileName(imageFileName);
        var uploads = Path.Combine(environmentWebRootPath, "images", "products");
        var filePath = Path.Combine(uploads, uniqueFileName);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        return filePath;
    }
}