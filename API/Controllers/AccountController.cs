using System.Security.Claims;
using API.Errors;
using Application.DTOs;
using Application.Interfaces;
using Core.Entities;
using Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager; // find the email 
    private readonly SignInManager<AppUser> _signInManager; // to check the signing password and user  
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
        
    /*
     *  "email": "Alex@test.com",
    "displayName": "Alex",
    "token": 
    "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.
    eyJlbWFpbCI6IkFsZXhAdGVzdC5jb20iLCJnaXZlbl9uYW1lIjoiQWxleCIs
    Im5iZiI6MTY3MzM1MTc4OCwiZXhwIjoxNjczOTU2NTg4LCJpYXQiOjE2NzMzNTE3ODgsImlzcyI
    6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEifQ.ILR4WZyHVSYmheGWMTnUboCV91UsG96GKpA3Bq4
    fBVfpGYWzgwUCLqdf5g_SOLJyNLsCIkVkCykO6f22WBHz2g"
     */
    [Authorize] //Specifies that the class or method that this attribute is applied to requires the specified authorization.
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        // var email = HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?
        //     .Value;
        var email = User.FindFirstValue(ClaimTypes.Email);
                    // we have access to the httpContext by nature 
        var user = await _userManager.FindByEmailAsync(email);
        
        return new UserDto
        {
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            DisplayName = user.DisplayName
        };
    }
        //
        /*
          eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
          eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.
          SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
         */
    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailIfExists(string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }
    
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
            Token = _tokenService.CreateToken(user),
            DisplayName = user.DisplayName
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        //check the database schema 
        var user = new AppUser {  
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email
           // PhoneNumber = registerDto.Email
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) { return BadRequest(new ApiResponse(400)); }

        return new UserDto
        {
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user),
            Email = user.Email
        }; 
    }
    
   
    //  [HttpPost("register")]
    // public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    // {
    //     if (await UserExists(registerDto.Username)) return BadRequest("User Name is Taken");
    //     
    //     using var hmac = new HMACSHA512();
    //     var user = new AppUser
    //     {
    //         UserName = registerDto.Username.ToLower() , 
    //         PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)) ,
    //         PasswordSalt = hmac.Key
    //     };
    //
    //     _context.Users.Add(user);
    //     await _context.SaveChangesAsync();
    //     return new UserDto
    //     {
    //         Username = user.UserName ,
    //         Token = _tokenService.CreateToken(user)
    //     };
    // }
    //
    // [HttpPost("login")]
    // public async Task<ActionResult<UserDto>> Login(LoginDTO loginDto)
    // {
    //     var user = await _context.Users.SingleOrDefaultAsync(z => z.UserName == loginDto.Username);
    //
    //     if (user == null) return Unauthorized("Invalid sername");
    //
    //     using var hmac = new HMACSHA512(user.PasswordSalt);
    //
    //     var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password)); // get the cumputed hash of this bite array 
    //
    //     for (int i = 0; i < computedHash.Length; i++)
    //     {
    //         if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password ");
    //         
    //     }
    //
    //     return new UserDto
    //     {
    //         Username = user.UserName ,
    //         Token = _tokenService.CreateToken(user)
    //     };
    // }
    

 
}