using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace frontend.Controllers
{
    public class VentasController : Controller
    {
        private readonly HttpClient _http;

        public VentasController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ventasApi");
        }

        [HttpGet]
        public async Task<IActionResult> Nueva()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario")))
                return RedirectToAction("Login", "Auth");

            try
            {
                await CargarDatosVenta();
                return View(new VentaViewModel { FechaVenta = DateTime.Now });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar datos: " + ex.Message;
                return View(new VentaViewModel { FechaVenta = DateTime.Now });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Nueva(VentaViewModel model)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario")))
                return RedirectToAction("Login", "Auth");

            try
            {
                Console.WriteLine("🔍 === INICIANDO REGISTRO DE VENTA ===");
                Console.WriteLine($"📦 ClienteId: {model.ClienteId}");
                Console.WriteLine($"📦 Fecha: {model.FechaVenta}");
                Console.WriteLine($"📦 DetallesJson: {model.DetallesJson}");

                if (model.ClienteId <= 0)
                {
                    ModelState.AddModelError("", "Debe seleccionar un cliente");
                    await CargarDatosVenta();
                    return View("Nueva", model);
                }

                if (string.IsNullOrEmpty(model.DetallesJson))
                {
                    ModelState.AddModelError("", "Debe agregar al menos un producto a la venta");
                    await CargarDatosVenta();
                    return View("Nueva", model);
                }

                var detalles = JsonConvert.DeserializeObject<List<DetalleVentaCreateRequest>>(model.DetallesJson);

                var ventaParaApi = new VentaCreateRequest
                {
                    clienteId = model.ClienteId,
                    fecha = model.FechaVenta == DateTime.MinValue ? DateTime.Now : model.FechaVenta,
                    detalles = detalles ?? new List<DetalleVentaCreateRequest>()
                };

                var response = await _http.PostAsJsonAsync("api/Sales", ventaParaApi);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Venta registrada exitosamente!";
                    return RedirectToAction("Historial", new { clienteId = model.ClienteId });
                }
                else
                {
                    var errorApi = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Error al registrar la venta: {errorApi}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error interno: " + ex.Message);
            }

            await CargarDatosVenta();
            return View("Nueva", model);
        }

        [HttpGet]
        public async Task<IActionResult> Historial(int? clienteId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario")))
                return RedirectToAction("Login", "Auth");

            try
            {
                var response = await _http.GetAsync("api/Sales");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var ventas = JsonConvert.DeserializeObject<List<VentaViewModel>>(json,
                    new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                if (clienteId.HasValue && clienteId > 0)
                {
                    ventas = ventas?.Where(v => v.ClienteId == clienteId.Value).ToList();
                    ViewBag.Titulo = $"Historial del Cliente ID: {clienteId}";
                }
                else
                {
                    ViewBag.Titulo = "Todas las Ventas";
                }

                return View(ventas ?? new List<VentaViewModel>());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar historial: " + ex.Message;
                ViewBag.Titulo = "Historial de Ventas";
                return View(new List<VentaViewModel>());
            }
        }

        private async Task CargarDatosVenta()
        {
            // Clientes
            var clientesResponse = await _http.GetAsync("api/Clients");
            clientesResponse.EnsureSuccessStatusCode();
            var clientesJson = await clientesResponse.Content.ReadAsStringAsync();
            ViewBag.Clientes = JsonConvert.DeserializeObject<List<ClienteViewModel>>(clientesJson);

            // Productos
            var productosResponse = await _http.GetAsync("api/Products");
            productosResponse.EnsureSuccessStatusCode();
            var productosJson = await productosResponse.Content.ReadAsStringAsync();
            ViewBag.Productos = JsonConvert.DeserializeObject<List<ProductoViewModel>>(productosJson);
        }
    }
}
