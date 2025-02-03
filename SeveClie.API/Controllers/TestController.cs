using Microsoft.AspNetCore.Mvc;
using SeveClie.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace SeveClie.API.Controllers
{
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("test-db-connection")]
        public async Task<IActionResult> TestDbConnection()
        {
            try
            {
                var tasks = await _context.Clie.ToListAsync();
                return Ok("Conexión a la base de datos exitosa.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error de conexión: {ex.Message}");
            }
        }
    }

}
