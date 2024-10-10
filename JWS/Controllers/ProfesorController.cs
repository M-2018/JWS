﻿using JWS.Data;
using JWS.DTOs;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : Controller
    {
        private readonly APIAppDbContext _context;

        public ProfesorController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/profesores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesorDTO>>> GetProfesores()
        {
            return await _context.Profesores
                .Select(p => new ProfesorDTO
                {
                    Id = p.Id,
                    Nombres = p.Nombres,
                    Apellidos = p.Apellidos,
                    NroDocumento = p.NroDocumento,
                    TipoDocumento = p.TipoDocumento,
                    FechaNacimiento = p.FechaNacimiento,
                    Direccion = p.Direccion,
                    Telefono = p.Telefono,
                    Email = p.Email,
                    Especialidad = p.Especialidad,
                    Activo = p.Activo
                })
                .ToListAsync();
        }

        // GET: api/profesores/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesorDTO>> GetProfesor(long id)
        {
            var profesor = await _context.Profesores.FindAsync(id);

            if (profesor == null)
            {
                return NotFound();
            }

            var profesorDTO = new ProfesorDTO
            {
                Id = profesor.Id,
                Nombres = profesor.Nombres,
                Apellidos = profesor.Apellidos,
                NroDocumento = profesor.NroDocumento,
                TipoDocumento = profesor.TipoDocumento,
                FechaNacimiento = profesor.FechaNacimiento,
                Direccion = profesor.Direccion,
                Telefono = profesor.Telefono,
                Email = profesor.Email,
                Especialidad = profesor.Especialidad,
                Activo = profesor.Activo
            };

            return profesorDTO;
        }

        // POST: api/profesores
        [HttpPost]
        public async Task<ActionResult<ProfesorDTO>> PostProfesor(ProfesorDTO profesorDTO)
        {
            var profesor = new Profesor
            {
                Nombres = profesorDTO.Nombres,
                Apellidos = profesorDTO.Apellidos,
                NroDocumento = profesorDTO.NroDocumento,
                TipoDocumento = profesorDTO.TipoDocumento,
                FechaNacimiento = profesorDTO.FechaNacimiento,
                Direccion = profesorDTO.Direccion,
                Telefono = profesorDTO.Telefono,
                Email = profesorDTO.Email,
                Especialidad = profesorDTO.Especialidad,
                Activo = profesorDTO.Activo
            };

            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();

            profesorDTO.Id = profesor.Id;

            return CreatedAtAction(nameof(GetProfesor), new { id = profesor.Id }, profesorDTO);
        }

        // PUT: api/profesores/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesor(long id, ProfesorDTO profesorDTO)
        {
            if (id != profesorDTO.Id)
            {
                return BadRequest();
            }

            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }

            profesor.Nombres = profesorDTO.Nombres;
            profesor.Apellidos = profesorDTO.Apellidos;
            profesor.NroDocumento = profesorDTO.NroDocumento;
            profesor.TipoDocumento = profesorDTO.TipoDocumento;
            profesor.FechaNacimiento = profesorDTO.FechaNacimiento;
            profesor.Direccion = profesorDTO.Direccion;
            profesor.Telefono = profesorDTO.Telefono;
            profesor.Email = profesorDTO.Email;
            profesor.Especialidad = profesorDTO.Especialidad;
            profesor.Activo = profesorDTO.Activo;

            _context.Entry(profesor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(id))
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

        // DELETE: api/profesores/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesor(long id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }

            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfesorExists(long id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }
    }
}
