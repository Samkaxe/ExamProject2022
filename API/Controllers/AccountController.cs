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
    
   

 
}