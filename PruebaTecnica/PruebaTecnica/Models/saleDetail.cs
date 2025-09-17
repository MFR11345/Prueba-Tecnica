namespace PruebaTecnica.Models
{
    public class saleDetail
    {
        public int detalleId {  get; set; }
        public int ventaId { get; set; }
        public Sale venta { get; set; } = null;
        public int productoId { get; set; }
        public Product producto { get; set; } = null;
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
    }
}
