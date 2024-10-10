using JWS.Data;
using JWS.DTOs;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly APIAppDbContext _context;

        public AsistenciaController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/asistencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsistenciaDTO>>> GetAsistencias()
        {
            return await _context.Asistencias
                .Select(a => new AsistenciaDTO
                {
                    Id = a.Id,
                    Fecha = a.Fecha,
                    Presente = a.Presente,
                    EstudianteId = a.EstudianteId,
                    MateriaId = a.MateriaId
                })
                .ToListAsync();
        }

        // GET: api/asistencias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AsistenciaDTO>> GetAsistencia(long id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);

            if (asistencia == null)
            {
                return NotFound();
            }

            var asistenciaDTO = new AsistenciaDTO
            {
                Id = asistencia.Id,
                Fecha = asistencia.Fecha,
                Presente = asistencia.Presente,
                EstudianteId = asistencia.EstudianteId,
                MateriaId = asistencia.MateriaId
            };

            return asistenciaDTO;
        }

        // POST: api/asistencias
        [HttpPost]
        public async Task<ActionResult<AsistenciaDTO>> PostAsistencia(AsistenciaDTO asistenciaDTO)
        {
            var asistencia = new Asistencia
            {
                Fecha = asistenciaDTO.Fecha,
                Presente = asistenciaDTO.Presente,
                EstudianteId = asistenciaDTO.EstudianteId,
                MateriaId = asistenciaDTO.MateriaId
            };

            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();

            asistenciaDTO.Id = asistencia.Id;

            return CreatedAtAction(nameof(GetAsistencia), new { id = asistenciaDTO.Id }, asistenciaDTO);
        }

        // PUT: api/asistencias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsistencia(long id, AsistenciaDTO asistenciaDTO)
        {
            if (id != asistenciaDTO.Id)
            {
                return BadRequest();
            }

            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }

            asistencia.Fecha = asistenciaDTO.Fecha;
            asistencia.Presente = asistenciaDTO.Presente;
            asistencia.EstudianteId = asistenciaDTO.EstudianteId;
            asistencia.MateriaId = asistenciaDTO.MateriaId;

            _context.Entry(asistencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsistenciaExists(id))
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

        // DELETE: api/asistencias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsistencia(long id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }

            _context.Asistencias.Remove(asistencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsistenciaExists(long id)
        {
            return _context.Asistencias.Any(e => e.Id == id);
        }
    }
}
