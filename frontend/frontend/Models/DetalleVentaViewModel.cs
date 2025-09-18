using Newtonsoft.Json;

namespace frontend.Models
{
    public class DetalleVentaViewModel
    {
        [JsonProperty("detalleId")]
        public int DetalleId { get; set; }

        [JsonProperty("productoId")]
        public int ProductoId { get; set; }

        // La API devuelve un objeto producto, extraemos el nombre
        [JsonIgnore]
        public string? ProductoNombre => Producto?.Nombre;

        [JsonProperty("producto")]
        public ProductoApiModel? Producto { get; set; }

        [JsonProperty("cantidad")]
        public int Cantidad { get; set; }

        [JsonProperty("precioUnitario")]
        public decimal Precio { get; set; }

        [JsonIgnore]
        public decimal Subtotal => Cantidad * Precio;
    }

    // Modelo para el producto que devuelve la API
    public class ProductoApiModel
    {
        [JsonProperty("productoId")]
        public int ProductoId { get; set; }

        [JsonProperty("nombre")]
        public string? Nombre { get; set; }

        [JsonProperty("precio")]
        public decimal Precio { get; set; }

        [JsonProperty("stock")]
        public int Stock { get; set; }
    }
}