using JWS.Data;
using JWS.DTOs;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionController : Controller
    {
        private readonly APIAppDbContext _context;

        public CalificacionController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Calificacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalificacionDTO>>> GetCalificaciones()
        {
            var calificaciones = await _context.Calificaciones
                .Select(c => new CalificacionDTO
                {
                    Id = c.Id,
                    NotaTrabajo1 = c.NotaTrabajo1,
                    NotaTrabajo2 = c.NotaTrabajo2,
                    NotaEvaluacion1 = c.NotaEvaluacion1,
                    NotaEvaluacion2 = c.NotaEvaluacion2,
                    NotaActitudinal = c.NotaActitudinal,
                    NotaExamenFinal = c.NotaExamenFinal,
                    NotaDefinitiva = c.NotaDefinitiva,
                    Recuperacion = c.Recuperacion,
                    NotaRecuperacion = c.NotaRecuperacion,
                    Habilitacion = c.Habilitacion,
                    NotaHabilitacion = c.NotaHabilitacion,
                    EstudianteId = c.EstudianteId,
                    CicloId = c.CicloId,
                    MateriaId = c.MateriaId
                })
                .ToListAsync();

            return Ok(calificaciones);
        }

        // GET: api/Calificacion/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CalificacionDTO>> GetCalificacion(long id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);

            if (calificacion == null)
            {
                return NotFound();
            }

            var calificacionDTO = new CalificacionDTO
            {
                Id = calificacion.Id,
                NotaTrabajo1 = calificacion.NotaTrabajo1,
                NotaTrabajo2 = calificacion.NotaTrabajo2,
                NotaEvaluacion1 = calificacion.NotaEvaluacion1,
                NotaEvaluacion2 = calificacion.NotaEvaluacion2,
                NotaActitudinal = calificacion.NotaActitudinal,
                NotaExamenFinal = calificacion.NotaExamenFinal,
                NotaDefinitiva = calificacion.NotaDefinitiva,
                Recuperacion = calificacion.Recuperacion,
                NotaRecuperacion = calificacion.NotaRecuperacion,
                Habilitacion = calificacion.Habilitacion,
                NotaHabilitacion = calificacion.NotaHabilitacion,
                EstudianteId = calificacion.EstudianteId,
                CicloId = calificacion.CicloId,
                MateriaId = calificacion.MateriaId
            };

            return Ok(calificacionDTO);
        }

        // POST: api/Calificacion
        [HttpPost]
        public async Task<ActionResult<CalificacionDTO>> CreateCalificacion(CalificacionDTO calificacionDTO)
        {
            var calificacion = new Calificacion
            {
                NotaTrabajo1 = calificacionDTO.NotaTrabajo1,
                NotaTrabajo2 = calificacionDTO.NotaTrabajo2,
                NotaEvaluacion1 = calificacionDTO.NotaEvaluacion1,
                NotaEvaluacion2 = calificacionDTO.NotaEvaluacion2,
                NotaActitudinal = calificacionDTO.NotaActitudinal,
                NotaExamenFinal = calificacionDTO.NotaExamenFinal,
                NotaDefinitiva = calificacionDTO.NotaDefinitiva,
                Recuperacion = calificacionDTO.Recuperacion,
                NotaRecuperacion = calificacionDTO.NotaRecuperacion,
                Habilitacion = calificacionDTO.Habilitacion,
                NotaHabilitacion = calificacionDTO.NotaHabilitacion,
                EstudianteId = calificacionDTO.EstudianteId,
                CicloId = calificacionDTO.CicloId,
                MateriaId = calificacionDTO.MateriaId
            };

            _context.Calificaciones.Add(calificacion);
            await _context.SaveChangesAsync();

            calificacionDTO.Id = calificacion.Id;

            return CreatedAtAction(nameof(GetCalificacion), new { id = calificacionDTO.Id }, calificacionDTO);
        }

        // PUT: api/Calificacion/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCalificacion(long id, CalificacionDTO calificacionDTO)
        {
            if (id != calificacionDTO.Id)
            {
                return BadRequest();
            }

            var calificacion = await _context.Calificaciones.FindAsync(id);

            if (calificacion == null)
            {
                return NotFound();
            }

            calificacion.NotaTrabajo1 = calificacionDTO.NotaTrabajo1;
            calificacion.NotaTrabajo2 = calificacionDTO.NotaTrabajo2;
            calificacion.NotaEvaluacion1 = calificacionDTO.NotaEvaluacion1;
            calificacion.NotaEvaluacion2 = calificacionDTO.NotaEvaluacion2;
            calificacion.NotaActitudinal = calificacionDTO.NotaActitudinal;
            calificacion.NotaExamenFinal = calificacionDTO.NotaExamenFinal;
            calificacion.NotaDefinitiva = calificacionDTO.NotaDefinitiva;
            calificacion.Recuperacion = calificacionDTO.Recuperacion;
            calificacion.NotaRecuperacion = calificacionDTO.NotaRecuperacion;
            calificacion.Habilitacion = calificacionDTO.Habilitacion;
            calificacion.NotaHabilitacion = calificacionDTO.NotaHabilitacion;
            calificacion.EstudianteId = calificacionDTO.EstudianteId;
            calificacion.CicloId = calificacionDTO.CicloId;
            calificacion.MateriaId = calificacionDTO.MateriaId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalificacionExists(id))
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

        // DELETE: api/Calificacion/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalificacion(long id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }

            _context.Calificaciones.Remove(calificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalificacionExists(long id)
        {
            return _context.Calificaciones.Any(e => e.Id == id);
        }
    }
}
