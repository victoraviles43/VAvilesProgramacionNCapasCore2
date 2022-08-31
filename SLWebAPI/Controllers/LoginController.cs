using Microsoft.AspNetCore.Mvc;

namespace SLWebAPI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
