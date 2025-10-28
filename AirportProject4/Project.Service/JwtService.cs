using AirportProject4.Project.core.Entities.Identity;
using AirportProject4.Project.core.ServiceContrect;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AirportProject4.Project.Service
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration configuration;

        public JwtService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateToken(AppUser appUser, UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,appUser.FullName),
                new Claim(ClaimTypes.Email,appUser.Email),
                
            };
            if(appUser != null)
            {
                var roles = await userManager.GetRolesAsync(appUser);

                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var Authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:AuthKey"] ?? string.Empty));

            var toke = new JwtSecurityToken
             (audience: configuration["Jwt:ValidAudience"],
               issuer: configuration["Jwt:ValidIssuer"],
               expires: DateTime.Now.AddDays(double.Parse(configuration["Jwt:DurationInDays"] ?? "0")),
               claims: authClaims,
               signingCredentials: new SigningCredentials(Authkey, SecurityAlgorithms.HmacSha256Signature)
             );

            return new JwtSecurityTokenHandler().WriteToken(toke);







        }
    }
}
