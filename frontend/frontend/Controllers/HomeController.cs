using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario")))
                return RedirectToAction("Login", "Auth");

            return View();
        }
    }
}