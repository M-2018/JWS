using JWS.Data;
using JWS.DTOs;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaResponsableController : Controller
    {
        private readonly APIAppDbContext _context;

        public PersonaResponsableController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonaResponsable
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaResponsableDTO>>> GetPersonaResponsables()
        {
            var responsables = await _context.PersonasResponsables
                .Include(r => r.Estudiante) // Incluir información del estudiante
                .ToListAsync();

            var responsablesDTO = responsables.Select(r => new PersonaResponsableDTO
            {
                Id = r.Id,
                Nombres = r.Nombres,
                Apellidos = r.Apellidos,
                Relacion = r.Relacion,
                EstudianteId = r.EstudianteId,
                Telefono = r.Telefono, // Añadir Telefono
                CorreoElectronico = r.CorreoElectronico // Añadir CorreoElectronico
            }).ToList();

            return Ok(responsablesDTO);
        }

        // GET: api/PersonaResponsable/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaResponsableDTO>> GetPersonaResponsable(long id)
        {
            var responsable = await _context.PersonasResponsables
                .Include(r => r.Estudiante) // Incluir información del estudiante
                .FirstOrDefaultAsync(r => r.Id == id);

            if (responsable == null)
            {
                return NotFound();
            }

            var responsableDTO = new PersonaResponsableDTO
            {
                Id = responsable.Id,
                Nombres = responsable.Nombres,
                Apellidos = responsable.Apellidos,
                Relacion = responsable.Relacion,
                EstudianteId = responsable.EstudianteId,
                Telefono = responsable.Telefono, // Añadir Telefono
                CorreoElectronico = responsable.CorreoElectronico // Añadir CorreoElectronico
            };

            return Ok(responsableDTO);
        }

        // POST: api/PersonaResponsable
        [HttpPost]
        public async Task<ActionResult<PersonaResponsableDTO>> PostPersonaResponsable(PersonaResponsableDTO responsableDTO)
        {
            var responsable = new PersonaResponsable
            {
                Nombres = responsableDTO.Nombres,
                Apellidos = responsableDTO.Apellidos,
                Relacion = responsableDTO.Relacion,
                EstudianteId = responsableDTO.EstudianteId,
                Telefono = responsableDTO.Telefono, // Añadir Telefono
                CorreoElectronico = responsableDTO.CorreoElectronico // Añadir CorreoElectronico
            };

            _context.PersonasResponsables.Add(responsable);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersonaResponsable), new { id = responsable.Id }, responsableDTO);
        }

        // PUT: api/PersonaResponsable/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaResponsable(long id, PersonaResponsableDTO responsableDTO)
        {
            if (id != responsableDTO.Id)
            {
                return BadRequest();
            }

            var responsable = await _context.PersonasResponsables.FindAsync(id);
            if (responsable == null)
            {
                return NotFound();
            }

            responsable.Nombres = responsableDTO.Nombres;
            responsable.Apellidos = responsableDTO.Apellidos;
            responsable.Relacion = responsableDTO.Relacion;
            responsable.EstudianteId = responsableDTO.EstudianteId;
            responsable.Telefono = responsableDTO.Telefono; // Actualizar Telefono
            responsable.CorreoElectronico = responsableDTO.CorreoElectronico; // Actualizar CorreoElectronico

            _context.Entry(responsable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaResponsableExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/PersonaResponsable/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonaResponsable(long id)
        {
            var responsable = await _context.PersonasResponsables.FindAsync(id);
            if (responsable == null)
            {
                return NotFound();
            }

            _context.PersonasResponsables.Remove(responsable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaResponsableExists(long id)
        {
            return _context.PersonasResponsables.Any(r => r.Id == id);
        }
    }
}
