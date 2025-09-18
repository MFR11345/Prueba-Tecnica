using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.DTOs;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly SalesService _service;
        public SalesController(AppDbContext db, SalesService service) { _db = db; _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.ventas.Include(v => v.Detalles).ThenInclude(d => d.producto).Include(v => v.cliente).ToListAsync());

        [HttpGet("client/{clienteId}")]
        public async Task<IActionResult> GetByClient(int clienteId)
        {
            var list = await _db.ventas.Where(v => v.clienteId == clienteId)
                                       .Include(v => v.Detalles).ThenInclude(d => d.producto)
                                       .ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] SaleCreateDto dto)
        {
            var (ok, error, sale) = await _service.CreateSaleAsync(dto);
            if (!ok) return BadRequest(new { error });
            return CreatedAtAction(nameof(GetAll), new { id = sale.ventaId }, sale);
        }
    }
}
