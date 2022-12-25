using System.Net.Sockets;
using Microsoft.AspNetCore.Identity;

namespace Core.Identity;

public class AppUser : IdentityUser // we dont need to spicify id for this entity because the class
    // that drive from have id already 
{
    /// <summary>
    /// Gets or sets a salted and hashed representation of the password for this user.
    /// </summary>
    // public virtual string PasswordHash { get; set; }
    public string DisplayName { get; set; }
    public Address Address { get; set; } 
    
}