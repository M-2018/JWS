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
            var ciclos = await _context.Ciclos
                .Include(c => c.CicloMaterias)
                .ThenInclude(cm => cm.Materia)
                .ToListAsync();

            var ciclosDTO = ciclos.Select(c => new CicloDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Anio = c.Anio,
                Semestre = c.Semestre,
                MateriasIds = c.CicloMaterias.Select(cm => cm.MateriaId).ToList()
            }).ToList();

            return Ok(ciclosDTO);
        }

        // GET: api/Ciclos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CicloDTO>> GetCiclo(long id)
        {
            var ciclo = await _context.Ciclos
                .Include(c => c.CicloMaterias)
                .ThenInclude(cm => cm.Materia)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (ciclo == null)
            {
                return NotFound();
            }

            var cicloDTO = new CicloDTO
            {
                Id = ciclo.Id,
                Nombre = ciclo.Nombre,
                Anio = ciclo.Anio,
                Semestre = ciclo.Semestre,
                MateriasIds = ciclo.CicloMaterias.Select(cm => cm.MateriaId).ToList()
            };

            return Ok(cicloDTO);
        }

        // POST: api/Ciclos
        [HttpPost]
        public async Task<ActionResult<CicloDTO>> PostCiclo(CicloDTO cicloDTO)
        {
            // Verificar que las materias existan
            var materias = await _context.Materias
                .Where(m => cicloDTO.MateriasIds.Contains(m.Id))
                .ToListAsync();

            if (materias.Count != cicloDTO.MateriasIds.Count)
            {
                return BadRequest("Una o más materias no existen.");
            }

            // Crear el ciclo
            var ciclo = new Ciclo
            {
                Nombre = cicloDTO.Nombre,
                Anio = cicloDTO.Anio,
                Semestre = cicloDTO.Semestre
            };

            // Crear la relación en la tabla intermedia CicloMateria para cada materia
            var cicloMaterias = materias.Select(m => new CicloMateria
            {
                MateriaId = m.Id,
                Ciclo = ciclo
            }).ToList();

            ciclo.CicloMaterias = cicloMaterias;

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

            var ciclo = await _context.Ciclos
                .Include(c => c.CicloMaterias)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (ciclo == null)
            {
                return NotFound();
            }

            // Actualizar datos del ciclo
            ciclo.Nombre = cicloDTO.Nombre;
            ciclo.Anio = cicloDTO.Anio;
            ciclo.Semestre = cicloDTO.Semestre;

            // Actualizar la relación de materias
            ciclo.CicloMaterias.Clear();
            var cicloMaterias = cicloDTO.MateriasIds.Select(mId => new CicloMateria
            {
                CicloId = id,
                MateriaId = mId
            }).ToList();
            ciclo.CicloMaterias = cicloMaterias;

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
            var ciclo = await _context.Ciclos
                .Include(c => c.CicloMaterias)
                .FirstOrDefaultAsync(c => c.Id == id);

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
