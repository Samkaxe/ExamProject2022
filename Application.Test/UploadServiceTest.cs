using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Moq;

using static Xunit.Assert;
namespace Application.Test;

public class UploadServiceTest
{
    private UploadService _service;
    private Mock<IFormFile> _fromFileMock;

    public UploadServiceTest()
    {
        _service = new UploadService();
        _fromFileMock = new Mock<IFormFile>();
    }

    [Fact]
    public async Task ShouldGetFilePathAfterSavingFileIntoWebRootPath()
    {
        var fileName = "file1";
        _fromFileMock.SetupGet(file => file.FileName).Returns(fileName+".jpg");
        var request = GetSampleRequest();
        await _service.SavePostImageAsync(request, "");
        var expectedPath = Path.Combine("", "images", "products", fileName);
        True(request.ImagePath.StartsWith(expectedPath));
    }

    [Fact]
    public async Task ShouldCopyUploadedImageToServerLocation()
    {
        var request = GetSampleRequest();
        await _service.SavePostImageAsync(request, "");
        _fromFileMock.Verify(file => file.CopyToAsync(
            It.IsAny<FileStream>(), CancellationToken.None),Times.Once());
    }
    [Fact]
    public async Task ShouldGetSuccessAndCombinedImagePathWhenCreatingPost()
    {
        var request = GetSampleRequest();
        request.ImagePath = "file1";
        var response = await _service.CreatePostAsync(request);
        True(response.Success);
        Equal(response.ImagePath,Path.Combine("images","products",request.ImagePath));
    }

    private UploadRequest GetSampleRequest()
    {
        return new UploadRequest
        {
            Image = _fromFileMock.Object,
            ImagePath = ""
        };
    }
}