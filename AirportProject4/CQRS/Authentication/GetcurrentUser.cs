using AirportProject4.Project.core.DTOS.AuthentivationDtos;
using AirportProject4.Project.core.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AirportProject4.CQRS.Authentication
{
    public record GetcurrentUser(AppUser AppUser) : MediatR.IRequest<USerDto?>;

    public class GetcurrentUserHandler:IRequestHandler<GetcurrentUser,USerDto?>
    {
        private readonly Project.core.ServiceContrect.IJwtService _jwtService;
        private readonly UserManager<AppUser> _userManager;
        public GetcurrentUserHandler(Project.core.ServiceContrect.IJwtService jwtService, UserManager<AppUser> userManager)
        {
            this._jwtService = jwtService;
            this._userManager = userManager;
        }
        public async Task<USerDto?> Handle(GetcurrentUser request, CancellationToken cancellationToken)
        {
            if (request.AppUser == null) return null;
            return new USerDto
            {
                DisplayName = request.AppUser.UserName,
                Email = request.AppUser.Email,
                PassportNumber = request.AppUser.PassportNumber,
                image = request.AppUser.ImageUrl?? "no image"
                
            };
        }
    }


}
