using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
    }
}
