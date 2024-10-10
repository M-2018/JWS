using JWS.Data;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : Controller
    {
        private readonly APIAppDbContext _context;

        public MateriaController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/materias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materia>>> GetMaterias()
        {
            return await _context.Materias.ToListAsync();
        }

        // GET: api/materias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Materia>> GetMateria(long id)
        {
            var materia = await _context.Materias.FindAsync(id);

            if (materia == null)
            {
                return NotFound();
            }

            return materia;
        }

        // POST: api/materias
        [HttpPost]
        public async Task<ActionResult<Materia>> PostMateria(Materia materia)
        {
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMateria), new { id = materia.Id }, materia);
        }

        // PUT: api/materias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateria(long id, Materia materia)
        {
            if (id != materia.Id)
            {
                return BadRequest();
            }

            _context.Entry(materia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaExists(id))
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

        // DELETE: api/materias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria(long id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }

            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MateriaExists(long id)
        {
            return _context.Materias.Any(e => e.Id == id);
        }
    }
}
