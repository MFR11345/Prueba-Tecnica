using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace frontend.Models
{
    public class VentaViewModel
    {
        [JsonProperty("ventaId")]
        public int VentaId { get; set; }

        [JsonProperty("fecha")]
        public DateTime FechaVenta { get; set; }

        [JsonProperty("clienteId")]
        public int ClienteId { get; set; }

        // La API devuelve un objeto cliente, extraemos el nombre
        [JsonIgnore]
        public string? ClienteNombre => Cliente?.Nombre;

        [JsonProperty("cliente")]
        public ClienteApiModel? Cliente { get; set; }

        [JsonProperty("detalles")]
        public List<DetalleVentaViewModel> Detalles { get; set; } = new List<DetalleVentaViewModel>();

        // NUEVA PROPiedad para recibir los detalles desde el formulario
        [JsonIgnore]
        public string? DetallesJson { get; set; }

        [JsonIgnore]
        public decimal Total => Detalles.Sum(d => d.Subtotal);
    }

    // Modelo para el cliente que devuelve la API
    public class ClienteApiModel
    {
        [JsonProperty("clienteId")]
        public int ClienteId { get; set; }

        [JsonProperty("nombre")]
        public string? Nombre { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }
    }

    public class VentaCreateRequest
    {
        public int clienteId { get; set; }
        public DateTime fecha { get; set; } = DateTime.Now;
        public List<DetalleVentaCreateRequest> detalles { get; set; } = new List<DetalleVentaCreateRequest>();
    }

    public class DetalleVentaCreateRequest
    {
        public int productoId { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
    }
}