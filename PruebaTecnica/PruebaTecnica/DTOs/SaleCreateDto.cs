namespace PruebaTecnica.DTOs
{
    public class SaleCreateDto
    {
        public DateTime fecha {  get; set; }
        public int? clienteId { get; set; }
        public ClientDto? Client { get; set; }
        public List<SaleItemDto> items { get; set; } = new();

    }
}
