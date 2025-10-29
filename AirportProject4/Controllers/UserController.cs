using AirportProject4.CQRS.Passnger.Commends;
using AirportProject4.Project.core.DTOS.ViewModles;
using AirportProject4.Project.core.Entities.Identity;
using AirportProject4.Project.core.Entities.main;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirportProject4.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IMediator mediator;

        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,IMediator mediator)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mediator = mediator;
        }

        public IActionResult UserIndex()
        {
            return View();
        }
       

      
       

      
    }
}
