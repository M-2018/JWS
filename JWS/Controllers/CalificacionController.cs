﻿using JWS.Data;
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
                    Taller = c.Taller,
                    Trabajo = c.Trabajo,
                    Exposicion = c.Exposicion,
                    Tarea = c.Tarea,
                    Quiz1 = c.Quiz1,
                    Quiz2 = c.Quiz2,
                    Actitudinal = c.Actitudinal,
                    ExamenFinal = c.ExamenFinal,
                    Definitiva = c.Definitiva,
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

        [HttpGet("Calificaciones")]
        public async Task<ActionResult<IEnumerable<CalificacionDTO>>> GetCalificaciones(long cicloId, long materiaId)
        {
            var calificaciones = await _context.Estudiantes
                .Where(e => e.CicloId == cicloId) // Filtrar por CicloId
                .Join(
                    _context.Calificaciones.Where(c => c.CicloId == cicloId && c.MateriaId == materiaId), // Filtrar las calificaciones por ciclo y materia
                    e => e.Id,
                    c => c.EstudianteId,
                    (e, c) => new
                    {
                        NombreCompleto = e.Nombres + " " + e.Apellidos,
                        Taller = c.Taller ?? 0,
                        Trabajo = c.Trabajo ?? 0,
                        Exposicion = c.Exposicion ?? 0,
                        Tarea = c.Tarea ?? 0,
                        Quiz1 = c.Quiz1 ?? 0,
                        Quiz2 = c.Quiz2 ?? 0,
                        Actitudinal = c.Actitudinal ?? 0,
                        ExamenFinal = c.ExamenFinal ?? 0,
                        Definitiva = c.Definitiva ?? 0,
                        NotaRecuperacion = c.NotaRecuperacion ?? 0,
                        NotaHabilitacion = c.NotaHabilitacion ?? 0,
                        CicloId = c.CicloId, // Incluir CicloId
                        MateriaId = c.MateriaId, // Incluir MateriaId
                        EstudianteId = c.EstudianteId,
                    })
                .Select(e => new CalificacionDTO
                {
                    NombreCompleto = e.NombreCompleto,
                    Taller = e.Taller,
                    Trabajo = e.Trabajo,
                    Exposicion = e.Exposicion,
                    Tarea = e.Tarea,
                    Quiz1 = e.Quiz1,
                    Quiz2 = e.Quiz2,
                    Actitudinal = e.Actitudinal,
                    ExamenFinal = e.ExamenFinal,
                    Definitiva = e.Definitiva,
                    NotaRecuperacion = e.NotaRecuperacion,
                    NotaHabilitacion = e.NotaHabilitacion,
                    CicloId = e.CicloId, // Incluir CicloId en el DTO
                    MateriaId = e.MateriaId, // Incluir MateriaId en el DTO
                    EstudianteId = e.EstudianteId,
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
                Taller = calificacion.Taller,
                Trabajo = calificacion.Trabajo,
                Exposicion = calificacion.Exposicion,
                Tarea = calificacion.Tarea,
                Quiz1 = calificacion.Quiz1,
                Quiz2 = calificacion.Quiz2,
                Actitudinal = calificacion.Actitudinal,
                ExamenFinal = calificacion.ExamenFinal,
                Definitiva = calificacion.Definitiva,
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

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CalificacionDTO>>> CreateCalificaciones(IEnumerable<CalificacionDTO> calificacionesDTO)
        {
            if (!calificacionesDTO.Any())
            {
                return BadRequest("No se proporcionaron calificaciones para guardar.");
            }

            if (calificacionesDTO.Any(c => c.EstudianteId <= 0 || c.CicloId <= 0 || c.MateriaId <= 0))
            {
                return BadRequest("Los IDs de estudiante, ciclo y materia deben ser valores positivos.");
            }

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

                    calificacionExistente.Taller = dto.Taller;
                    calificacionExistente.Trabajo = dto.Trabajo;
                    calificacionExistente.Exposicion = dto.Exposicion;
                    calificacionExistente.Tarea = dto.Tarea;
                    calificacionExistente.Quiz1 = dto.Quiz1;
                    calificacionExistente.Quiz2 = dto.Quiz2;
                    calificacionExistente.Actitudinal = dto.Actitudinal;
                    calificacionExistente.ExamenFinal = dto.ExamenFinal;
                    calificacionExistente.Definitiva = dto.Definitiva;
                    calificacionExistente.Recuperacion = dto.Recuperacion;
                    calificacionExistente.NotaRecuperacion = dto.NotaRecuperacion;
                    calificacionExistente.Habilitacion = dto.Habilitacion;
                    calificacionExistente.NotaHabilitacion = dto.NotaHabilitacion;
                    calificacionExistente.EstudianteId = dto.EstudianteId;
                    calificacionExistente.CicloId = dto.CicloId;
                    calificacionExistente.MateriaId = dto.MateriaId;

                    _context.Calificaciones.Update(calificacionExistente);
                }
                else
                {
                    // Crear nueva calificación
                    Console.WriteLine($"Creando nueva calificación para estudiante {dto.EstudianteId}");

                    var nuevaCalificacion = new Calificacion
                    {
                        Taller = dto.Taller,
                        Trabajo = dto.Trabajo,
                        Exposicion = dto.Exposicion,
                        Tarea = dto.Tarea,
                        Quiz1 = dto.Quiz1,
                        Quiz2 = dto.Quiz2,
                        Actitudinal = dto.Actitudinal,
                        ExamenFinal = dto.ExamenFinal,
                        Definitiva = dto.Definitiva,
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
                        Taller = c.Taller,
                        Trabajo = c.Trabajo,
                        Exposicion = c.Exposicion,
                        Tarea = c.Tarea,
                        Quiz1 = c.Quiz1,
                        Quiz2 = c.Quiz2,
                        Actitudinal = c.Actitudinal,
                        ExamenFinal = c.ExamenFinal,
                        Definitiva = c.Definitiva,
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
                    calificacion.Taller = dto.Taller;
                    calificacion.Trabajo = dto.Trabajo;
                    calificacion.Exposicion = dto.Exposicion;
                    calificacion.Tarea = dto.Tarea;
                    calificacion.Quiz1 = dto.Quiz1;
                    calificacion.Quiz2 = dto.Quiz2;
                    calificacion.Actitudinal = dto.Actitudinal;
                    calificacion.ExamenFinal = dto.ExamenFinal;
                    calificacion.Definitiva = dto.Definitiva;
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
