using Core.Identity;

namespace Application.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}