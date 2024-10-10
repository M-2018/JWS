using JWS.Data;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : Controller
    {
        private readonly APIAppDbContext _context;

        public ProfesorController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/profesores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesores()
        {
            return await _context.Profesores.ToListAsync();
        }

        // GET: api/profesores/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesor>> GetProfesor(long id)
        {
            var profesor = await _context.Profesores.FindAsync(id);

            if (profesor == null)
            {
                return NotFound();
            }

            return profesor;
        }

        // POST: api/profesores
        [HttpPost]
        public async Task<ActionResult<Profesor>> PostProfesor(Profesor profesor)
        {
            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfesor), new { id = profesor.Id }, profesor);
        }

        // PUT: api/profesores/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesor(long id, Profesor profesor)
        {
            if (id != profesor.Id)
            {
                return BadRequest();
            }

            _context.Entry(profesor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(id))
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

        // DELETE: api/profesores/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesor(long id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }

            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfesorExists(long id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }
    }
}
