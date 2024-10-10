using JWS.Data;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly APIAppDbContext _context;

        public AsistenciaController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/asistencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asistencia>>> GetAsistencias()
        {
            return await _context.Asistencias.ToListAsync();
        }

        // GET: api/asistencias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Asistencia>> GetAsistencia(long id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);

            if (asistencia == null)
            {
                return NotFound();
            }

            return asistencia;
        }

        // POST: api/asistencias
        [HttpPost]
        public async Task<ActionResult<Asistencia>> PostAsistencia(Asistencia asistencia)
        {
            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAsistencia), new { id = asistencia.Id }, asistencia);
        }

        // PUT: api/asistencias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsistencia(long id, Asistencia asistencia)
        {
            if (id != asistencia.Id)
            {
                return BadRequest();
            }

            _context.Entry(asistencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsistenciaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/asistencias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsistencia(long id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }

            _context.Asistencias.Remove(asistencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsistenciaExists(long id)
        {
            return _context.Asistencias.Any(e => e.Id == id);
        }
    }
}
