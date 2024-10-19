using JWS.Data;
using JWS.DTOs;
using JWS.Enums;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiclosController : ControllerBase
    {
        private readonly APIAppDbContext _context;

        public CiclosController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Ciclos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CicloDTO>>> GetCiclos()
        {
            var ciclos = await _context.Ciclos.ToListAsync();
            var ciclosDTO = ciclos.Select(c => new CicloDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Anio = c.Anio,
                Semestre = c.Semestre
            }).ToList();

            return Ok(ciclosDTO);
        }

        // GET: api/Ciclos/5
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
                Nombre = ciclo.Nombre,
                Anio = ciclo.Anio,
                Semestre = ciclo.Semestre
            };

            return Ok(cicloDTO);
        }

        // POST: api/Ciclos
        [HttpPost]
        public async Task<ActionResult<CicloDTO>> PostCiclo(CicloDTO cicloDTO)
        {
            var ciclo = new Ciclo
            {
                Nombre = cicloDTO.Nombre,
                Anio = cicloDTO.Anio,
                Semestre = cicloDTO.Semestre
            };

            _context.Ciclos.Add(ciclo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCiclo), new { id = ciclo.Id }, cicloDTO);
        }

        // PUT: api/Ciclos/5
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
            ciclo.Anio = cicloDTO.Anio;
            ciclo.Semestre = cicloDTO.Semestre;

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
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Ciclos/5
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
