using AirportProject4.Project.core;
using AirportProject4.Project.core.Entities.Identity;
using AirportProject4.Project.core.Entities.main;
using AirportProject4.Shared.Specification;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AirportProject4.CQRS.Passnger.Query
{
    public class GetPassngerbyPassportNumberQuery
    {
        public record GetPassngerbyNumberQuery(string passspotNumber) : IRequest<AppUser>;

        public class GetPassngerbyNumberHandler : IRequestHandler<GetPassngerbyNumberQuery, AppUser>
        {
           
            private readonly UserManager<AppUser> userManager;

            public GetPassngerbyNumberHandler( UserManager<AppUser> userManager)
            {
                this.userManager = userManager;
            }
            public async Task<AppUser?> Handle(GetPassngerbyNumberQuery request, CancellationToken cancellationToken)
            {
                var user = await userManager.Users.FirstOrDefaultAsync(u => u.PassportNumber == request.passspotNumber);

                if (user == null) return null;
                return user;

            }
        }
    }
}
