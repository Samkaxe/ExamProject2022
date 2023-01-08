using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using static Xunit.Assert;  //I added this line to do not repeat "Assert" to use its methods example Assert.True()====>True() in line 29

namespace Application.Test;

public class UploadServiceTest
{
    
    private UploadService _service;
    private Mock<IFormFile> _fromFileMock;  //mocking IfromFile interface. this interface is used in UploadedRequest class

    public UploadServiceTest()
    {
        _service = new UploadService();
        _fromFileMock = new Mock<IFormFile>();
    }
//this test checks file path after saving uploaded file will return or not
    [Fact]
    public async Task ShouldGetFilePathAfterSavingFileIntoWebRootPath()
    {
        var fileName = "file1";
        _fromFileMock.SetupGet(file => file.FileName).Returns(fileName+".jpg");  //setup mock to return "file1.jpg" when getting mockedFile.FileName
        var request = GetSampleRequest();                                        //generating a dummy request
        await _service.SavePostImageAsync(request, "");                         //calling upload service method
        var expectedPath = Path.Combine("", "images", "products", fileName);    //gnerating expected path
        True(request.ImagePath.StartsWith(expectedPath));                      
        //checking if result path is started with expected path (we did not use equal because uploaded file name is unique and generated dynamically)
    }

//this test is for ensure that uploaded file will be saved in server location
    [Fact]
    public async Task ShouldCopyUploadedImageToServerLocation()
    {
        var request = GetSampleRequest();  //genrating dummy mocked request
        await _service.SavePostImageAsync(request, "");     //calling service upload method
        _fromFileMock.Verify(file => file.CopyToAsync(  
            It.IsAny<FileStream>(), CancellationToken.None),Times.Once());          //verifying that file.copyAsync is called once or not
    }

    //this method is to check that is success filed has set in response or not  and image path is set.
    [Fact]
    public async Task ShouldGetSuccessAndCombinedImagePathWhenCreatingPost()
    {
        var request = GetSampleRequest();
        request.ImagePath = "file1";
        var response = await _service.CreatePostAsync(request);
        True(response.Success);     //check that success field is true
        Equal(response.ImagePath,Path.Combine("images","products",request.ImagePath));  //check that response image path is in correct way
    }

///this private method returns a dummy request object with mocked Image
    private UploadRequest GetSampleRequest()
    {
        return new UploadRequest
        {
            Image = _fromFileMock.Object,   //using mock object for image
            ImagePath = ""
        };
    }
}