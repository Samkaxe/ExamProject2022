using Core.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public  class AppIdebtityContextSeed
{
   /// <summary>
   /// Provides the APIs for managing user in a persistence store.
   /// </summary>
   /// <typeparam name="TUser">The type encapsulating a user.</typeparam>
   public static async Task SeedUserAsync(UserManager<AppUser> userManager) // todo configure it in the startup class 
   {
      if (!userManager.Users.Any())
      {
         var user = new AppUser
         {
            DisplayName = "Alex",
            Email = "Alex@test.com",
            UserName = "Alex@test.com",
            Address = new Address
            {
               FirstName = "ALex",
               LastName = "uldahl",
               City = "esbjerg",
               ZipCode = "6700",

            }
         };
         /*
          *  dollar dollar zero.  
            Ardie because the password needs to have at least one uppercase letter, at least one lowercase letter,
             at least one non alphanumeric character, and also a numeric character as well.
          */
                                // password should contain dollar 
         await userManager.CreateAsync(user, "Pa$$w0rd");
      }
   }
   /*
    * UserManager<AppUser> userManager, 
         RoleManager<IdentityRole> roleManager)
        {
            string role = "Admin";
 
            var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                }
 
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com"
                 
                };
 
                IdentityResult identityResult = userManager.CreateAsync(user, "Pa$$w0rd").Result;
 
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }            
            }
        }
    */
}