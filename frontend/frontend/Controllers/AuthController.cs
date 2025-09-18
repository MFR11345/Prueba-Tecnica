using frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            // Si ya está autenticado, redirigir al home
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario")))
                return RedirectToAction("Index", "Home");

            return View();
        }


        [HttpPost]
        // REMOVE THIS TEMPORARILY: [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            // DEBUG: Ver en consola del servidor
            Console.WriteLine($"🔍 Login attempt: {model.Email}");

            if (model.Email == "admin@ventas.com" && model.Password == "admin123")
            {
                // DEBUG
                Console.WriteLine("✅ Login successful! Redirecting to Home...");

                HttpContext.Session.SetString("Usuario", model.Email);
                return RedirectToAction("Index", "Home"); // ← Aquí debe redirigir
            }

            // DEBUG
            Console.WriteLine("❌ Login failed");
            return View(model);
        }
    }
}