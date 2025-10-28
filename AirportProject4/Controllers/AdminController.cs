using AirportProject4.CQRS.Passnger.Commends;
using AirportProject4.Project.core.DTOS.ViewModles;
using AirportProject4.Project.core.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AirportProject4.Controllers
{
    public class AdminController : Controller
    {

        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IMediator mediator;

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMediator mediator)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mediator = mediator;
        }
        public IActionResult AdminIndex()
        {
            return View();
        }


        public IActionResult CreateNewUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewUser(string FullName, string Email, string Password, IFormFile? Image = null)
        {
            try
            {
                var User = await mediator.Send(new CreatePassngerCommend(FullName, Email, Password, Image));
                if (User == null)
                {
                    ModelState.AddModelError(string.Empty, "Error while creating user");
                    return View();
                }
                return View(User);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"error while creating user{ex.Message}");

                return View();
            }

        }

        public IActionResult Createrole()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Createrole(RoleviewModle model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                foreach (var e in errors)
                {
                    Console.WriteLine($"Validation error: {e.ErrorMessage}");
                }
                return View(model);
            }
            try
            {
                var role = new AppRole { Id = model.Id, Name = model.RoleName };

                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Flight", "Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                return View(model);
            }

        }
    }
}
