using Microsoft.AspNetCore.Mvc;

namespace SLWebAPI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string pasword, string email)
        {
            ML.Result result = BL.Usuario.GetByIdEmail(email);
            if (result.Correct)
            {
                ML.Usuario usuario = ((ML.Usuario)result.Object);
                if (usuario.Pasword == pasword)
                {
                   
                    RedirectToAction("Login","Home");
                  
                    
                }
                else
                {
                    ViewBag.Message = "La contraseña no existe";
                    return PartialView("Modal");
                }

            }
            else
            {
                ViewBag.Message = "La contraseña no existe";
                return PartialView("Modal");
            }
            return View();
        }

   
    }

}
