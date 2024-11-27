﻿using JWS.Data;
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
        //[HttpPost]
        //public async Task<ActionResult<Asistencia>> PostAsistencia(AsistenciaDTO asistenciaDTO)
        //{
        //    var asistencia = new Asistencia
        //    {
        //        Fecha = asistenciaDTO.Fecha,
        //        Presente = asistenciaDTO.Presente,
        //        EstudianteId = asistenciaDTO.EstudianteId,
        //        MateriaId = asistenciaDTO.MateriaId,
        //        CicloId = asistenciaDTO.CicloId
        //    };

        //    _context.Asistencias.Add(asistencia);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetAsistencia), new { id = asistencia.Id }, asistencia);
        //}

        // POST: api/Asistencias
        [HttpPost]
        public async Task<ActionResult> PostAsistencias(IEnumerable<AsistenciaDTO> asistenciasDTO)
        {
            if (asistenciasDTO == null || !asistenciasDTO.Any())
            {
                return BadRequest("Debe proporcionar al menos un registro.");
            }

            var asistencias = asistenciasDTO.Select(dto => new Asistencia
            {
                Fecha = dto.Fecha,
                Presente = dto.Presente,
                EstudianteId = dto.EstudianteId,
                MateriaId = dto.MateriaId,
                CicloId = dto.CicloId
            }).ToList();

            await _context.Asistencias.AddRangeAsync(asistencias);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAsistencias), null);
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

        // GET: api/Asistencias/filtrar
        //Filtra asistencias por fecha, ciclo y materia
        [HttpGet("filtrar")]
        public async Task<ActionResult<IEnumerable<AsistenciaDTO>>> FiltrarAsistencias([FromQuery] long? cicloId, [FromQuery] DateTime? fecha, [FromQuery] long? materiaId)
        {
            var query = _context.Asistencias.AsQueryable();

            if (cicloId.HasValue)
            {
                query = query.Where(a => a.CicloId == cicloId.Value);
            }

            if (fecha.HasValue)
            {
                query = query.Where(a => a.Fecha.HasValue && a.Fecha.Value.Date == fecha.Value.Date);
            }

            if (materiaId.HasValue)
            {
                query = query.Where(a => a.MateriaId == materiaId.Value);
            }

            var asistenciasFiltradas = await query
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

            if (!asistenciasFiltradas.Any())
            {
                return NotFound("No se encontraron asistencias con los filtros proporcionados.");
            }

            return Ok(asistenciasFiltradas);
        }

    }
}
