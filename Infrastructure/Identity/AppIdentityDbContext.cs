using Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class AppIdentityDbContext : IdentityDbContext<AppUser>
{
    // dotnet ef migrations add IdentityInitial -p Infrastructure -s API  -c AppIdentityDbContext -o Identity/Migrations
    // -P SPICIFY the infrastructure 
    // -s spicify the start up class
    //  -c switch spificy thr dbcontect 
    // -o the output 
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
    {
    }
    // if we dont add this we will have an issue with the Id field 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    // we dont need to set DBset here because IdentityDbcontext will take care of that by default 
}