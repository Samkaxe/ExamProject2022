using API.Models;
using Application.Helper;
using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public class UploadService : IUploadService
{
    public async Task SavePostImageAsync(UploadRequest uploadRequest, string environmentWebRootPath)
    {
        var uniqueFileName = FileHelper.GetUniqueFileName(uploadRequest.Image.FileName);

        var uploads = Path.Combine(environmentWebRootPath, "images", "products");
        var filePath = Path.Combine(uploads, uniqueFileName);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        await uploadRequest.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
        uploadRequest.ImagePath = filePath;
    }

    public async Task<UploadResponse> CreatePostAsync(UploadRequest postRequest)
    {
        return new UploadResponse
            {Success = true, ImagePath = Path.Combine("images", "products", Path.GetFileName(postRequest.ImagePath))};
    }
}