using System.Security.Claims;
using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class UserManagerExyinstion
{
    // extend the usermanager to get more methods 
    public static async Task<AppUser> FindByUserByClaimsPrincipleEmailWithAddressAync(this UserManager<AppUser> input
        , ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email);

        return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email ==
            email);
    }

    public static async Task<AppUser> FindByEmailFromClaimsPrinciple(this UserManager<AppUser>
        input, ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email);
        
        return await input.Users.SingleOrDefaultAsync(x => x.Email ==
                                                           email);
    }
    
}