using AirportProject4.Project.core.DTOS.ViewModles;
using AirportProject4.Project.core.Entities.Identity;
using AirportProject4.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;

namespace AirportProject4.CQRS.Authentication
{
    public record Signup(RigsterviewModler signupDto) : IRequest<IdentityResult>;

    public class singupHandler:IRequestHandler<Signup, IdentityResult>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public singupHandler(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IdentityResult> Handle(Signup request, CancellationToken cancellationToken)
        {
            var User = new AppUser()
            {
                UserName = request.signupDto.UserName,
                FullName=request.signupDto.UserName,
                Email = request.signupDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PassportNumber = GenertaPasswordHelper.GeneratePassportNumber()
            };
            var result= await userManager.CreateAsync(User, request.signupDto.Password);

            return result;

        }
    }


}
