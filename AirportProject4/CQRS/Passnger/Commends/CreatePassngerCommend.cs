using AirportProject4.Project.core;
using AirportProject4.Project.core.Entities.Identity;
using AirportProject4.Project.core.Entities.main;
using AirportProject4.Shared;
using AirportProject4.Shared.attachmentService;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AirportProject4.CQRS.Passnger.Commends
{
    public record CreatePassngerCommend(string FullName, string Email, string Password, IFormFile? Image = null) : IRequest<AppUser>;

    public class CreatePassngerCommendHandler : IRequestHandler<CreatePassngerCommend, AppUser>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IattachmentService attachmentService;

        public CreatePassngerCommendHandler( UserManager<AppUser> userManager,IattachmentService attachmentService)
        {
            this.userManager = userManager;
            this.attachmentService = attachmentService;
        }
        public async Task<AppUser> Handle(CreatePassngerCommend request, CancellationToken cancellationToken)
        {
            // تحقق من تكرار الإيميل
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                throw new Exception("User with this email already exists");

            var passnger = new AppUser()
            {
                FullName = request.FullName,
                UserName = request.Email,
                Email = request.Email,
                PassportNumber = GenertaPasswordHelper.GeneratePassportNumber(),
                EmailConfirmed = true
            };

            if (request.Image != null)
            {
                passnger.ImageUrl = attachmentService.UploadImage(request.Image, "Images");
            }

            var result = await userManager.CreateAsync(passnger, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Error while adding passenger: {errors}");
            }

            await userManager.AddToRoleAsync(passnger, "Passenger");

            return passnger;
        }
    }
}
