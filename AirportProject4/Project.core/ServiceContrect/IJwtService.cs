using AirportProject4.Project.core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AirportProject4.Project.core.ServiceContrect
{
    public interface IJwtService
    {
        Task<string> CreateToken(AppUser appUser,UserManager<AppUser> userManager);
    }
}
