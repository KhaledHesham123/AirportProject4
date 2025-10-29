using Microsoft.AspNetCore.Mvc;

namespace AirportProject4.Controllers
{
    public class SuperadminController : Controller
    {
        public IActionResult SuperadminIndex()
        {
            return View();
        }
    }
}
