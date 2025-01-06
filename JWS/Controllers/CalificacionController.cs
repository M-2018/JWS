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

        //[HttpPost]
        //public async Task<ActionResult<IEnumerable<CalificacionDTO>>> CreateCalificaciones(IEnumerable<CalificacionDTO> calificacionesDTO)
        //{
        //    var calificaciones = calificacionesDTO.Select(dto => new Calificacion
        //    {
        //        NotaTrabajo1 = dto.NotaTrabajo1,
        //        NotaTrabajo2 = dto.NotaTrabajo2,
        //        NotaEvaluacion1 = dto.NotaEvaluacion1,
        //        NotaEvaluacion2 = dto.NotaEvaluacion2,
        //        NotaActitudinal = dto.NotaActitudinal,
        //        NotaExamenFinal = dto.NotaExamenFinal,
        //        NotaDefinitiva = dto.NotaDefinitiva,
        //        Recuperacion = dto.Recuperacion,
        //        NotaRecuperacion = dto.NotaRecuperacion,
        //        Habilitacion = dto.Habilitacion,
        //        NotaHabilitacion = dto.NotaHabilitacion,
        //        EstudianteId = dto.EstudianteId,
        //        CicloId = dto.CicloId,
        //        MateriaId = dto.MateriaId
        //    }).ToList();

        //    await _context.Calificaciones.AddRangeAsync(calificaciones);
        //    await _context.SaveChangesAsync();

        //    var createdDTOs = calificaciones.Select(c => new CalificacionDTO
        //    {
        //        Id = c.Id,
        //        NotaTrabajo1 = c.NotaTrabajo1,
        //        NotaTrabajo2 = c.NotaTrabajo2,
        //        NotaEvaluacion1 = c.NotaEvaluacion1,
        //        NotaEvaluacion2 = c.NotaEvaluacion2,
        //        NotaActitudinal = c.NotaActitudinal,
        //        NotaExamenFinal = c.NotaExamenFinal,
        //        NotaDefinitiva = c.NotaDefinitiva,
        //        Recuperacion = c.Recuperacion,
        //        NotaRecuperacion = c.NotaRecuperacion,
        //        Habilitacion = c.Habilitacion,
        //        NotaHabilitacion = c.NotaHabilitacion,
        //        EstudianteId = c.EstudianteId,
        //        CicloId = c.CicloId,
        //        MateriaId = c.MateriaId
        //    }).ToList();

        //    return CreatedAtAction(nameof(GetCalificaciones), createdDTOs);
        //}

        //[HttpPost]
        //public async Task<ActionResult<IEnumerable<CalificacionDTO>>> CreateCalificaciones(IEnumerable<CalificacionDTO> calificacionesDTO)
        //{
        //    var calificacionesActualizadas = new List<Calificacion>();
        //    var calificacionesNuevas = new List<Calificacion>();

        //    foreach (var dto in calificacionesDTO)
        //    {
        //        // Buscar si ya existe una calificación para este estudiante en esta materia y ciclo
        //        var calificacionExistente = await _context.Calificaciones
        //            .FirstOrDefaultAsync(c =>
        //                c.EstudianteId == dto.EstudianteId &&
        //                c.MateriaId == dto.MateriaId &&
        //                c.CicloId == dto.CicloId);

        //        if (calificacionExistente != null)
        //        {
        //            // Actualizar la calificación existente
        //            calificacionExistente.NotaTrabajo1 = dto.NotaTrabajo1;
        //            calificacionExistente.NotaTrabajo2 = dto.NotaTrabajo2;
        //            calificacionExistente.NotaEvaluacion1 = dto.NotaEvaluacion1;
        //            calificacionExistente.NotaEvaluacion2 = dto.NotaEvaluacion2;
        //            calificacionExistente.NotaActitudinal = dto.NotaActitudinal;
        //            calificacionExistente.NotaExamenFinal = dto.NotaExamenFinal;
        //            calificacionExistente.NotaDefinitiva = dto.NotaDefinitiva;
        //            calificacionExistente.Recuperacion = dto.Recuperacion;
        //            calificacionExistente.NotaRecuperacion = dto.NotaRecuperacion;
        //            calificacionExistente.Habilitacion = dto.Habilitacion;
        //            calificacionExistente.NotaHabilitacion = dto.NotaHabilitacion;

        //            calificacionesActualizadas.Add(calificacionExistente);
        //        }
        //        else
        //        {
        //            // Crear nueva calificación
        //            var nuevaCalificacion = new Calificacion
        //            {
        //                NotaTrabajo1 = dto.NotaTrabajo1,
        //                NotaTrabajo2 = dto.NotaTrabajo2,
        //                NotaEvaluacion1 = dto.NotaEvaluacion1,
        //                NotaEvaluacion2 = dto.NotaEvaluacion2,
        //                NotaActitudinal = dto.NotaActitudinal,
        //                NotaExamenFinal = dto.NotaExamenFinal,
        //                NotaDefinitiva = dto.NotaDefinitiva,
        //                Recuperacion = dto.Recuperacion,
        //                NotaRecuperacion = dto.NotaRecuperacion,
        //                Habilitacion = dto.Habilitacion,
        //                NotaHabilitacion = dto.NotaHabilitacion,
        //                EstudianteId = dto.EstudianteId,
        //                CicloId = dto.CicloId,
        //                MateriaId = dto.MateriaId
        //            };

        //            calificacionesNuevas.Add(nuevaCalificacion);
        //        }
        //    }

        //    // Agregar nuevas calificaciones si existen
        //    if (calificacionesNuevas.Any())
        //    {
        //        await _context.Calificaciones.AddRangeAsync(calificacionesNuevas);
        //    }

        //    try
        //    {
        //        await _context.SaveChangesAsync();

        //        // Combinar todas las calificaciones para la respuesta
        //        var todasLasCalificaciones = calificacionesActualizadas.Concat(calificacionesNuevas);

        //        // Convertir a DTOs para la respuesta
        //        var responseDto = todasLasCalificaciones.Select(c => new CalificacionDTO
        //        {
        //            Id = c.Id,
        //            NotaTrabajo1 = c.NotaTrabajo1,
        //            NotaTrabajo2 = c.NotaTrabajo2,
        //            NotaEvaluacion1 = c.NotaEvaluacion1,
        //            NotaEvaluacion2 = c.NotaEvaluacion2,
        //            NotaActitudinal = c.NotaActitudinal,
        //            NotaExamenFinal = c.NotaExamenFinal,
        //            NotaDefinitiva = c.NotaDefinitiva,
        //            Recuperacion = c.Recuperacion,
        //            NotaRecuperacion = c.NotaRecuperacion,
        //            Habilitacion = c.Habilitacion,
        //            NotaHabilitacion = c.NotaHabilitacion,
        //            EstudianteId = c.EstudianteId,
        //            CicloId = c.CicloId,
        //            MateriaId = c.MateriaId
        //        });

        //        return Ok(responseDto);
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        return BadRequest("Error al guardar las calificaciones: " + ex.InnerException?.Message);
        //    }
        //}
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CalificacionDTO>>> CreateCalificaciones(IEnumerable<CalificacionDTO> calificacionesDTO)
        {
            // Primero, obtenemos todos los IDs relevantes
            var estudianteIds = calificacionesDTO.Select(c => c.EstudianteId).ToList();
            var materiaId = calificacionesDTO.First().MateriaId;
            var cicloId = calificacionesDTO.First().CicloId;

            // Buscamos todas las calificaciones existentes para estos estudiantes en esta materia y ciclo
            var calificacionesExistentes = await _context.Calificaciones
                .Where(c =>
                    estudianteIds.Contains(c.EstudianteId) &&
                    c.MateriaId == materiaId &&
                    c.CicloId == cicloId)
                .ToListAsync();

            // Para diagnóstico
            Console.WriteLine($"Calificaciones existentes encontradas: {calificacionesExistentes.Count}");

            foreach (var dto in calificacionesDTO)
            {
                var calificacionExistente = calificacionesExistentes
                    .FirstOrDefault(c =>
                        c.EstudianteId == dto.EstudianteId &&
                        c.MateriaId == dto.MateriaId &&
                        c.CicloId == dto.CicloId);

                if (calificacionExistente != null)
                {
                    // Actualizar la calificación existente
                    Console.WriteLine($"Actualizando calificación para estudiante {dto.EstudianteId}");

                    calificacionExistente.NotaTrabajo1 = dto.NotaTrabajo1;
                    calificacionExistente.NotaTrabajo2 = dto.NotaTrabajo2;
                    calificacionExistente.NotaEvaluacion1 = dto.NotaEvaluacion1;
                    calificacionExistente.NotaEvaluacion2 = dto.NotaEvaluacion2;
                    calificacionExistente.NotaActitudinal = dto.NotaActitudinal;
                    calificacionExistente.NotaExamenFinal = dto.NotaExamenFinal;
                    calificacionExistente.NotaDefinitiva = dto.NotaDefinitiva;
                    calificacionExistente.Recuperacion = dto.Recuperacion;
                    calificacionExistente.NotaRecuperacion = dto.NotaRecuperacion;
                    calificacionExistente.Habilitacion = dto.Habilitacion;
                    calificacionExistente.NotaHabilitacion = dto.NotaHabilitacion;

                    _context.Calificaciones.Update(calificacionExistente);
                }
                else
                {
                    // Crear nueva calificación
                    Console.WriteLine($"Creando nueva calificación para estudiante {dto.EstudianteId}");

                    var nuevaCalificacion = new Calificacion
                    {
                        NotaTrabajo1 = dto.NotaTrabajo1,
                        NotaTrabajo2 = dto.NotaTrabajo2,
                        NotaEvaluacion1 = dto.NotaEvaluacion1,
                        NotaEvaluacion2 = dto.NotaEvaluacion2,
                        NotaActitudinal = dto.NotaActitudinal,
                        NotaExamenFinal = dto.NotaExamenFinal,
                        NotaDefinitiva = dto.NotaDefinitiva,
                        Recuperacion = dto.Recuperacion,
                        NotaRecuperacion = dto.NotaRecuperacion,
                        Habilitacion = dto.Habilitacion,
                        NotaHabilitacion = dto.NotaHabilitacion,
                        EstudianteId = dto.EstudianteId,
                        CicloId = dto.CicloId,
                        MateriaId = dto.MateriaId
                    };

                    await _context.Calificaciones.AddAsync(nuevaCalificacion);
                }
            }

            try
            {
                await _context.SaveChangesAsync();

                // Obtener todas las calificaciones actualizadas
                var calificacionesActualizadas = await _context.Calificaciones
                    .Where(c =>
                        estudianteIds.Contains(c.EstudianteId) &&
                        c.MateriaId == materiaId &&
                        c.CicloId == cicloId)
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

                return Ok(calificacionesActualizadas);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error al guardar las calificaciones: {ex.Message}");
                Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                return BadRequest($"Error al guardar las calificaciones: {ex.Message}");
            }
        }



        [HttpPut]
        public async Task<IActionResult> UpdateCalificaciones(IEnumerable<CalificacionDTO> calificacionesDTO)
        {
            var ids = calificacionesDTO.Select(dto => dto.Id).ToList();
            var calificaciones = await _context.Calificaciones.Where(c => ids.Contains(c.Id)).ToListAsync();

            if (calificaciones.Count != calificacionesDTO.Count())
            {
                return NotFound("Algunas calificaciones no fueron encontradas.");
            }

            foreach (var dto in calificacionesDTO)
            {
                var calificacion = calificaciones.FirstOrDefault(c => c.Id == dto.Id);
                if (calificacion != null)
                {
                    calificacion.NotaTrabajo1 = dto.NotaTrabajo1;
                    calificacion.NotaTrabajo2 = dto.NotaTrabajo2;
                    calificacion.NotaEvaluacion1 = dto.NotaEvaluacion1;
                    calificacion.NotaEvaluacion2 = dto.NotaEvaluacion2;
                    calificacion.NotaActitudinal = dto.NotaActitudinal;
                    calificacion.NotaExamenFinal = dto.NotaExamenFinal;
                    calificacion.NotaDefinitiva = dto.NotaDefinitiva;
                    calificacion.Recuperacion = dto.Recuperacion;
                    calificacion.NotaRecuperacion = dto.NotaRecuperacion;
                    calificacion.Habilitacion = dto.Habilitacion;
                    calificacion.NotaHabilitacion = dto.NotaHabilitacion;
                    calificacion.EstudianteId = dto.EstudianteId;
                    calificacion.CicloId = dto.CicloId;
                    calificacion.MateriaId = dto.MateriaId;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Error al actualizar los registros.");
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
