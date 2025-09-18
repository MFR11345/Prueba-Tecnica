using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace frontend.Controllers
{
    public class ProductosController : Controller
    {
        private readonly HttpClient _http;

        public ProductosController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ventasApi");
        }

        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario")))
                return RedirectToAction("Login", "Auth");

            try
            {
                var response = await _http.GetAsync("api/Products");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var productos = JsonConvert.DeserializeObject<List<ProductoViewModel>>(json);

                return View(productos);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar productos: " + ex.Message;
                return View(new List<ProductoViewModel>());
            }
        }
    }
}