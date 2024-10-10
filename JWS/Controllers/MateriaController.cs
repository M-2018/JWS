using JWS.Data;
using JWS.DTOs;
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
        public async Task<ActionResult<IEnumerable<MateriaDTO>>> GetMaterias()
        {
            return await _context.Materias
                .Select(m => new MateriaDTO
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    ProfesorId = m.ProfesorId,
                    CicloId = m.CicloId
                })
                .ToListAsync();
        }

        // GET: api/materias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaDTO>> GetMateria(long id)
        {
            var materia = await _context.Materias.FindAsync(id);

            if (materia == null)
            {
                return NotFound();
            }

            var materiaDTO = new MateriaDTO
            {
                Id = materia.Id,
                Nombre = materia.Nombre,
                ProfesorId = materia.ProfesorId,
                CicloId = materia.CicloId
            };

            return materiaDTO;
        }

        // POST: api/materias
        [HttpPost]
        public async Task<ActionResult<MateriaDTO>> PostMateria(MateriaDTO materiaDTO)
        {
            var materia = new Materia
            {
                Nombre = materiaDTO.Nombre,
                ProfesorId = materiaDTO.ProfesorId,
                CicloId = materiaDTO.CicloId
            };

            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();

            materiaDTO.Id = materia.Id;

            return CreatedAtAction(nameof(GetMateria), new { id = materiaDTO.Id }, materiaDTO);
        }

        // PUT: api/materias/{id}
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
            materia.CicloId = materiaDTO.CicloId;

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
