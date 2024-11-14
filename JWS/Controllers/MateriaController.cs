using JWS.Data;
using JWS.DTOs;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                .Include(m => m.Profesor) // Incluir información del profesor
                .Include(m => m.CicloMaterias) // Incluir los ciclos asociados
                .ThenInclude(cm => cm.Ciclo) // Incluir la información del ciclo
                .ToListAsync();

            var materiasDTO = materias.Select(m => new MateriaDTO
            {
                Id = m.Id,
                Nombre = m.Nombre,
                ProfesorId = m.ProfesorId,
                CicloId = m.CicloMaterias.Select(cm => cm.CicloId).FirstOrDefault() // Obtener el primer ciclo asociado
            }).ToList();

            return Ok(materiasDTO);
        }

        // GET: api/Materia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaDTO>> GetMateria(long id)
        {
            var materia = await _context.Materias
                .Include(m => m.Profesor) // Incluir información del profesor
                .Include(m => m.CicloMaterias) // Incluir los ciclos asociados
                .ThenInclude(cm => cm.Ciclo) // Incluir la información del ciclo
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
                CicloId = materia.CicloMaterias.Select(cm => cm.CicloId).FirstOrDefault() // Obtener el primer ciclo asociado
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

            // Asociar la materia al ciclo
            var cicloMateria = new CicloMateria
            {
                MateriaId = materia.Id,
                CicloId = materiaDTO.CicloId
            };
            _context.CicloMaterias.Add(cicloMateria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMateria), new { id = materia.Id }, materiaDTO);
        }

        // PUT: api/Materia/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMateria(long id, MateriaDTO materiaDTO)
        //{
        //    if (id != materiaDTO.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var materia = await _context.Materias.FindAsync(id);
        //    if (materia == null)
        //    {
        //        return NotFound();
        //    }

        //    materia.Nombre = materiaDTO.Nombre;
        //    materia.ProfesorId = materiaDTO.ProfesorId;

        //    _context.Entry(materia).State = EntityState.Modified;

        //    // Actualizar la relación con CicloMateria
        //    var cicloMateria = await _context.CicloMaterias
        //        .FirstOrDefaultAsync(cm => cm.MateriaId == id);
        //    if (cicloMateria != null)
        //    {
        //        cicloMateria.CicloId = materiaDTO.CicloId;
        //        _context.Entry(cicloMateria).State = EntityState.Modified;
        //    }

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MateriaExists(id))
        //        {
        //            return NotFound();
        //        }
        //        throw;
        //    }

        //    return NoContent();
        //}

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
            var nuevoCicloMateria = new CicloMateria
            {
                MateriaId = id,
                CicloId = materiaDTO.CicloId
            };
            _context.CicloMaterias.Add(nuevoCicloMateria);

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
                .FirstOrDefaultAsync(cm => cm.MateriaId == id);
            if (cicloMateria != null)
            {
                _context.CicloMaterias.Remove(cicloMateria);
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
