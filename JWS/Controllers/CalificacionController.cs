using JWS.Data;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionController : Controller
    {
        private readonly APIAppDbContext _context;

        public CalificacionController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/calificaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calificacion>>> GetCalificaciones()
        {
            return await _context.Calificaciones.ToListAsync();
        }

        // GET: api/calificaciones/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Calificacion>> GetCalificacion(long id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);

            if (calificacion == null)
            {
                return NotFound();
            }

            return calificacion;
        }

        // POST: api/calificaciones
        [HttpPost]
        public async Task<ActionResult<Calificacion>> PostCalificacion(Calificacion calificacion)
        {
            _context.Calificaciones.Add(calificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCalificacion), new { id = calificacion.Id }, calificacion);
        }

        // PUT: api/calificaciones/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalificacion(long id, Calificacion calificacion)
        {
            if (id != calificacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(calificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalificacionExists(id))
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

        // DELETE: api/calificaciones/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalificacion(long id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }

            _context.Calificaciones.Remove(calificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalificacionExists(long id)
        {
            return _context.Calificaciones.Any(e => e.Id == id);
        }
    }
}
