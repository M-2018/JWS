using JWS.Data;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CicloController : Controller
    {
        private readonly APIAppDbContext _context;

        public CicloController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/ciclos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ciclo>>> GetCiclos()
        {
            return await _context.Ciclos.ToListAsync();
        }

        // GET: api/ciclos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Ciclo>> GetCiclo(long id)
        {
            var ciclo = await _context.Ciclos.FindAsync(id);

            if (ciclo == null)
            {
                return NotFound();
            }

            return ciclo;
        }

        // POST: api/ciclos
        [HttpPost]
        public async Task<ActionResult<Ciclo>> PostCiclo(Ciclo ciclo)
        {
            _context.Ciclos.Add(ciclo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCiclo), new { id = ciclo.Id }, ciclo);
        }

        // PUT: api/ciclos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCiclo(long id, Ciclo ciclo)
        {
            if (id != ciclo.Id)
            {
                return BadRequest();
            }

            _context.Entry(ciclo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CicloExists(id))
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

        // DELETE: api/ciclos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCiclo(long id)
        {
            var ciclo = await _context.Ciclos.FindAsync(id);
            if (ciclo == null)
            {
                return NotFound();
            }

            _context.Ciclos.Remove(ciclo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CicloExists(long id)
        {
            return _context.Ciclos.Any(e => e.Id == id);
        }
    }
}
