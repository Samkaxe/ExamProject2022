using API.Models;
using Application.Models;

namespace Application.Interfaces;

public interface IUploadService
{
    Task SavePostImageAsync(UploadRequest uploadRequest, string environmentWebRootPath);
    Task<UploadResponse> CreatePostAsync(UploadRequest postRequest);
}