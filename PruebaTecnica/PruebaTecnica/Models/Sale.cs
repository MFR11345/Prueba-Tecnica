namespace PruebaTecnica.Models
{
    public class Sale
    {
        public int ventaId { get; set; }
        public DateTime fecha {  get; set; }
        public int clienteId { get; set; }
        public cliente cliente { get; set; } = null;
        public List<saleDetail> Detalles { get; set; } = new();
    }
}
