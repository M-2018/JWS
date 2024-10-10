using JWS.Data;
using JWS.DTOs;
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
        public async Task<ActionResult<IEnumerable<CicloDTO>>> GetCiclos()
        {
            return await _context.Ciclos
                .Select(c => new CicloDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
                .ToListAsync();
        }

        // GET: api/ciclos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CicloDTO>> GetCiclo(long id)
        {
            var ciclo = await _context.Ciclos.FindAsync(id);

            if (ciclo == null)
            {
                return NotFound();
            }

            var cicloDTO = new CicloDTO
            {
                Id = ciclo.Id,
                Nombre = ciclo.Nombre
            };

            return cicloDTO;
        }

        // POST: api/ciclos
        [HttpPost]
        public async Task<ActionResult<CicloDTO>> PostCiclo(CicloDTO cicloDTO)
        {
            var ciclo = new Ciclo
            {
                Nombre = cicloDTO.Nombre
            };

            _context.Ciclos.Add(ciclo);
            await _context.SaveChangesAsync();

            cicloDTO.Id = ciclo.Id;

            return CreatedAtAction(nameof(GetCiclo), new { id = cicloDTO.Id }, cicloDTO);
        }

        // PUT: api/ciclos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCiclo(long id, CicloDTO cicloDTO)
        {
            if (id != cicloDTO.Id)
            {
                return BadRequest();
            }

            var ciclo = await _context.Ciclos.FindAsync(id);
            if (ciclo == null)
            {
                return NotFound();
            }

            ciclo.Nombre = cicloDTO.Nombre;

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
