using JWS.Data;
using JWS.DTOs;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly APIAppDbContext _context;

        public MateriaController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Materia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaDTO>>> GetMaterias()
        {
            var materias = await _context.Materias
                .Include(m => m.Profesor)
                .Include(m => m.CicloMaterias)
                .ThenInclude(cm => cm.Ciclo)
                .ToListAsync();

            var materiasDTO = materias.Select(m => new MateriaDTO
            {
                Id = m.Id,
                Nombre = m.Nombre,
                ProfesorId = m.ProfesorId,
                CicloId = m.CicloMaterias.Select(cm => cm.CicloId).FirstOrDefault() // Tomar el primer ciclo asociado
            }).ToList();

            return Ok(materiasDTO);
        }

        // GET: api/Materia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaDTO>> GetMateria(long id)
        {
            var materia = await _context.Materias
                .Include(m => m.Profesor)
                .Include(m => m.CicloMaterias)
                .ThenInclude(cm => cm.Ciclo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (materia == null)
            {
                return NotFound();
            }

            var materiaDTO = new MateriaDTO
            {
                Id = materia.Id,
                Nombre = materia.Nombre,
                ProfesorId = materia.ProfesorId,
                CicloId = materia.CicloMaterias.Select(cm => cm.CicloId).FirstOrDefault()
            };

            return Ok(materiaDTO);
        }

        // POST: api/Materia
        [HttpPost]
        public async Task<ActionResult<MateriaDTO>> PostMateria(MateriaDTO materiaDTO)
        {
            var materia = new Materia
            {
                Nombre = materiaDTO.Nombre,
                ProfesorId = materiaDTO.ProfesorId
            };

            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();

            // Validar y asociar la materia al ciclo si se proporciona un CicloId
            if (materiaDTO.CicloId > 0)
            {
                var cicloMateria = new CicloMateria
                {
                    MateriaId = materia.Id,
                    CicloId = materiaDTO.CicloId
                };
                _context.CicloMaterias.Add(cicloMateria);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetMateria), new { id = materia.Id }, materiaDTO);
        }

        // PUT: api/Materia/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateria(long id, MateriaDTO materiaDTO)
        {
            if (id != materiaDTO.Id)
            {
                return BadRequest();
            }

            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }

            materia.Nombre = materiaDTO.Nombre;
            materia.ProfesorId = materiaDTO.ProfesorId;

            _context.Entry(materia).State = EntityState.Modified;

            // Eliminar la relación actual con CicloMateria
            var cicloMateria = await _context.CicloMaterias
                .FirstOrDefaultAsync(cm => cm.MateriaId == id);
            if (cicloMateria != null)
            {
                _context.CicloMaterias.Remove(cicloMateria);
            }

            // Crear la nueva relación con el CicloId proporcionado
            if (materiaDTO.CicloId > 0)
            {
                var nuevoCicloMateria = new CicloMateria
                {
                    MateriaId = id,
                    CicloId = materiaDTO.CicloId
                };
                _context.CicloMaterias.Add(nuevoCicloMateria);
            }

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
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Materia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria(long id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }

            // Eliminar la relación de CicloMateria
            var cicloMateria = await _context.CicloMaterias
                .Where(cm => cm.MateriaId == id)
                .ToListAsync();
            if (cicloMateria.Any())
            {
                _context.CicloMaterias.RemoveRange(cicloMateria);
            }

            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MateriaExists(long id)
        {
            return _context.Materias.Any(m => m.Id == id);
        }
    }
}
