using AirportProject4.CQRS.Authentication;
using AirportProject4.Project.core.DTOS.AuthentivationDtos;
using AirportProject4.Project.core.DTOS.ViewModles;
using AirportProject4.Project.core.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AirportProject4.Controllers
{
    public class AcountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AcountController(IMediator mediator,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this._mediator = mediator;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Signup(RigsterviewModler rigsterviewModler) 
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new Signup(rigsterviewModler));

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Signin));
                }

                // لو التسجيل فشل، اعرض كل الأخطاء في الـ ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(rigsterviewModler);

        }
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(LoginViewModle loginViewModle)
        {
            if (ModelState.IsValid)
            {
                var signinResult = await _mediator.Send(new login(loginViewModle));
                if (signinResult !=null)
                {
                    return RedirectToAction(nameof(FlightController.Index), "Flight");
                }
                ModelState.AddModelError(string.Empty, "Invalid login");
            }
            return View(loginViewModle);

        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                await _mediator.Send(new logoutCommned());
                return RedirectToAction(nameof(Signin));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logout failed: {ex.Message}");

                return RedirectToAction(nameof(Signin));


            }
        }

        public async Task<IActionResult> getCurentUser()
        {

            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                // لو المستخدم مش مسجّل الدخول
                if (string.IsNullOrEmpty(userEmail))
                    return RedirectToAction("Signin", "Acount");

                var appUser = await userManager.FindByEmailAsync(userEmail);

                // لو المستخدم مش موجود في قاعدة البيانات
                if (appUser == null)
                    return RedirectToAction("Signin", "Acount");

                var user = await _mediator.Send(new GetcurrentUser(appUser));

                // لو الميدييتور رجع null
                if (user == null)
                    return RedirectToAction("Index", "Home");

                // ✅ رجّع الموديل للـ View
                return View(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
