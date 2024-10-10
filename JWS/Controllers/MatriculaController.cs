using JWS.Data;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaController : Controller
    {
        private readonly APIAppDbContext _context;

        public MatriculaController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/matriculas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matricula>>> GetMatriculas()
        {
            return await _context.Matriculas.ToListAsync();
        }

        // GET: api/matriculas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Matricula>> GetMatricula(long id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);

            if (matricula == null)
            {
                return NotFound();
            }

            return matricula;
        }

        // POST: api/matriculas
        [HttpPost]
        public async Task<ActionResult<Matricula>> PostMatricula(Matricula matricula)
        {
            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMatricula), new { id = matricula.Id }, matricula);
        }

        // PUT: api/matriculas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatricula(long id, Matricula matricula)
        {
            if (id != matricula.Id)
            {
                return BadRequest();
            }

            _context.Entry(matricula).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatriculaExists(id))
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

        // DELETE: api/matriculas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatricula(long id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatriculaExists(long id)
        {
            return _context.Matriculas.Any(e => e.Id == id);
        }
    }
}
