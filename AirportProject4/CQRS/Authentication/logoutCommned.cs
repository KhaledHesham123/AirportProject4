using AirportProject4.Project.core.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AirportProject4.CQRS.Authentication
{
    public record logoutCommned:IRequest<bool>;

    public class logoutcommnedHandler : IRequestHandler<logoutCommned, bool>
    {
        private readonly SignInManager<AppUser> signInManager;

        public logoutcommnedHandler(SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<bool> Handle(logoutCommned request, CancellationToken cancellationToken)
        {
           var result=  signInManager.SignOutAsync();

            return true;
        }
    }


}
