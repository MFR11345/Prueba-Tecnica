using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace frontend.Controllers
{
    public class ClientesController : Controller
    {
        private readonly HttpClient _http;

        public ClientesController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ventasApi");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _http.GetAsync("api/clientes");
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var clientes = JsonConvert.DeserializeObject<List<ClienteViewModel>>(json);
                    
                    // Asegurar que nunca sea null
                    return View(clientes ?? new List<ClienteViewModel>());
                }
                else
                {
                    // Si la API falla, devolver lista vacía
                    return View(new List<ClienteViewModel>());
                }
            }
            catch (Exception ex)
            {
                // En caso de error, devolver lista vacía
                return View(new List<ClienteViewModel>());
            }
        }
    }
}