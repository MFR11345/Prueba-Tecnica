using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ClientsController(AppDbContext db) => _db = db;

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.Clientes.ToListAsync());

    }
}
