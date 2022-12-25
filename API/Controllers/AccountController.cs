using API.Errors;
using Application.DTOs;
using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    /*
* {
"errors": [
   "The Email field is not a valid e-mail address.",
   "The Password field is required."
],
"statusCode": 400,
"message": "A bad request , you have made"
}
*/
    
    /*
     *  "errors": [
        "Email address is in use"
    ],
    "statusCode": 400,
    "message": "A bad request , you have made"
}
     */
    // Attempts a password sign in for a user.
    //     Params:
    // user – The user to sign in.
    // password – The password to attempt to sign in with.
    //     lockoutOnFailure – Flag indicating if the user account should be locked if the sign in fails.
    //     Returns:
    // The task object representing the asynchronous operation containing the for the sign-in attempt.

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }  
       
        var result = await _signInManager.CheckPasswordSignInAsync(user , loginDto.Password , false);

        if (!result.Succeeded)
        {
            return Unauthorized(new ApiResponse(401));
            
        }
        
        return new UserDto
        {
            Email = user.Email,
            Token = "Tested fake Token",
            DisplayName = user.DisplayName
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var user = new AppUser {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) { return BadRequest(new ApiResponse(400)); }

        return new UserDto
        {
            DisplayName = user.DisplayName,
            Token = "this is fake Token",
            Email = user.Email
        }; 
    }

    /*
    * "email": "Alex@test.com",
   "displayName": "Alex",
   "token": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IkFsZXhAdGVzdC5jb20iLCJnaXZlbl9uYW1
   lIjoiQWxleCIsIm5iZiI6MTY2NTk1ODExOSwiZXhwIjoxNjY2NTYyOTE5LCJpYXQiOjE2NjU5NTgxMTksImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEifQ.JIf2DixvHqPdnLLBgEDM3YhorhKIHsVdkKqFeImrM0iCIKfcYA9xwoOZx_APbovW8QPzaqwqhgKqCeyZGBJ1xQ"
   {
 "alg": "HS512",
 "typ": "JWT"
 }
  {
        "email": "Alex@test.com",
        "given_name": "Alex",
        "nbf": 1665958119,
        "exp": 1666562919,
        "iat": 1665958119,
        "iss": "https://localhost:5001"
  }

           HMACSHA512(
           base64UrlEncode(header) + "." +
           base64UrlEncode(payload),
           
         your-256-bit-secret

          ) secret base64 encoded
   
      */
}