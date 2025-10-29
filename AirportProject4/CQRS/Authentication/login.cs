using AirportProject4.Project.core.DTOS.AuthentivationDtos;
using AirportProject4.Project.core.DTOS.ViewModles;
using AirportProject4.Project.core.Entities.Identity;
using AirportProject4.Project.core.ServiceContrect;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AirportProject4.CQRS.Authentication
{
    public record login(LoginViewModle loginDto) : IRequest<USerToreturnDto?>;

    public class loginHandler : IRequestHandler<login, USerToreturnDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService jwtService;

        public loginHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtService jwtService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.jwtService = jwtService;
        }
        public async Task<USerToreturnDto?> Handle(login request, CancellationToken cancellationToken)
        {
            var User = await _userManager.FindByEmailAsync(request.loginDto.Email);
            if (User == null) return null;

            var flag = await _userManager.CheckPasswordAsync(User, request.loginDto.Password);

            if (!flag) { return null; }
            var result = await _signInManager.
             PasswordSignInAsync(User, request.loginDto.Password, request.loginDto.RememberMe, false);
            if (!result.Succeeded) return null;
            var roles = await _userManager.GetRolesAsync(User);


            return new USerToreturnDto
            {
                DisplayName = User.UserName??"null",
                Email = User.Email ?? "null",
                Token = await jwtService.CreateToken(User, _userManager),
                role = roles.FirstOrDefault()?? "null"
            };
        }
    }


}
