using API.Models;
using Application.Helper;
using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public class UploadService : IUploadService
{
    //this method gets uploadRequest and root path then save uploaded file to specific path in Server
    public async Task SavePostImageAsync(UploadRequest uploadRequest, string environmentWebRootPath)
    {
        // gets path to save the uploaded file
        var filePath = GetFilePath(environmentWebRootPath, uploadRequest.Image.FileName);
        //save uploaded file to generated path
        await uploadRequest.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
        //set image path of request to uploaded file path to use it for later
        uploadRequest.ImagePath = filePath;
    }
    
    //this method generates a response object from input request and add a success field to response and set image path with relative address
    public async Task<UploadResponse> CreatePostAsync(UploadRequest postRequest)
    {
        return new UploadResponse
            //example c:\uploadedFiles\images\a.jpg ===> images/products/a.jpg
            {Success = true, ImagePath = Path.Combine("images", "products", Path.GetFileName(postRequest.ImagePath))};
    }
    
    //this private method is used to generate a file path with unique name    
    private static string GetFilePath(string environmentWebRootPath, string imageFileName)
    {
        var uniqueFileName = FileHelper.GetUniqueFileName(imageFileName); //generates a unique name exampe a.jpg===>a356.jpg
        var uploads = Path.Combine(environmentWebRootPath, "images", "products"); // creating upload folder addresss
        var filePath = Path.Combine(uploads, uniqueFileName);  //combining upload folder path and unique file name
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)); //create upload folder if not exist
        return filePath;
    }
}