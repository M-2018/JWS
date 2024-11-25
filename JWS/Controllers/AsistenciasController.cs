using JWS.Data;
using JWS.DTOs;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciasController : Controller
    {
        private readonly APIAppDbContext _context;

        public AsistenciasController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Asistencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsistenciaDTO>>> GetAsistencias()
        {
            var asistencias = await _context.Asistencias
                .Select(a => new AsistenciaDTO
                {
                    Id = a.Id,
                    Fecha = a.Fecha,
                    Presente = a.Presente,
                    EstudianteId = a.EstudianteId,
                    MateriaId = a.MateriaId,
                    CicloId = a.CicloId
                })
                .ToListAsync();

            return Ok(asistencias);
        }

        // GET: api/Asistencias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AsistenciaDTO>> GetAsistencia(long id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);

            if (asistencia == null)
            {
                return NotFound();
            }

            return new AsistenciaDTO
            {
                Id = asistencia.Id,
                Fecha = asistencia.Fecha,
                Presente = asistencia.Presente,
                EstudianteId = asistencia.EstudianteId,
                MateriaId = asistencia.MateriaId,
                CicloId = asistencia.CicloId
            };
        }

        // POST: api/Asistencias
        [HttpPost]
        public async Task<ActionResult<Asistencia>> PostAsistencia(AsistenciaDTO asistenciaDTO)
        {
            var asistencia = new Asistencia
            {
                Fecha = asistenciaDTO.Fecha,
                Presente = asistenciaDTO.Presente,
                EstudianteId = asistenciaDTO.EstudianteId,
                MateriaId = asistenciaDTO.MateriaId,
                CicloId = asistenciaDTO.CicloId
            };

            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAsistencia), new { id = asistencia.Id }, asistencia);
        }

        // PUT: api/Asistencias/{id}
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
            asistencia.CicloId = asistenciaDTO.CicloId;

            _context.Entry(asistencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Asistencias.Any(a => a.Id == id))
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

        // DELETE: api/Asistencias/{id}
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
    }
}
