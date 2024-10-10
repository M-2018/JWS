using JWS.Data;
using JWS.DTOs;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaResponsableController : ControllerBase
    {
        private readonly APIAppDbContext _context;

        public PersonaResponsableController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/personasresponsables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaResponsableDTO>>> GetPersonasResponsables()
        {
            return await _context.PersonasResponsables
                .Select(pr => new PersonaResponsableDTO
                {
                    Id = pr.Id,
                    Nombres = pr.Nombres,
                    Apellidos = pr.Apellidos,
                    Relacion = pr.Relacion,
                    EstudianteId = pr.EstudianteId
                })
                .ToListAsync();
        }

        // GET: api/personasresponsables/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaResponsableDTO>> GetPersonaResponsable(long id)
        {
            var personaResponsable = await _context.PersonasResponsables.FindAsync(id);

            if (personaResponsable == null)
            {
                return NotFound();
            }

            var personaResponsableDTO = new PersonaResponsableDTO
            {
                Id = personaResponsable.Id,
                Nombres = personaResponsable.Nombres,
                Apellidos = personaResponsable.Apellidos,
                Relacion = personaResponsable.Relacion,
                EstudianteId = personaResponsable.EstudianteId
            };

            return personaResponsableDTO;
        }

        // POST: api/personasresponsables
        [HttpPost]
        public async Task<ActionResult<PersonaResponsableDTO>> PostPersonaResponsable(PersonaResponsableDTO personaResponsableDTO)
        {
            var personaResponsable = new PersonaResponsable
            {
                Nombres = personaResponsableDTO.Nombres,
                Apellidos = personaResponsableDTO.Apellidos,
                Relacion = personaResponsableDTO.Relacion,
                EstudianteId = personaResponsableDTO.EstudianteId
            };

            _context.PersonasResponsables.Add(personaResponsable);
            await _context.SaveChangesAsync();

            personaResponsableDTO.Id = personaResponsable.Id;

            return CreatedAtAction(nameof(GetPersonaResponsable), new { id = personaResponsableDTO.Id }, personaResponsableDTO);
        }

        // PUT: api/personasresponsables/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaResponsable(long id, PersonaResponsableDTO personaResponsableDTO)
        {
            if (id != personaResponsableDTO.Id)
            {
                return BadRequest();
            }

            var personaResponsable = await _context.PersonasResponsables.FindAsync(id);
            if (personaResponsable == null)
            {
                return NotFound();
            }

            personaResponsable.Nombres = personaResponsableDTO.Nombres;
            personaResponsable.Apellidos = personaResponsableDTO.Apellidos;
            personaResponsable.Relacion = personaResponsableDTO.Relacion;
            personaResponsable.EstudianteId = personaResponsableDTO.EstudianteId;

            _context.Entry(personaResponsable).State = EntityState.Modified;

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
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/personasresponsables/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonaResponsable(long id)
        {
            var personaResponsable = await _context.PersonasResponsables.FindAsync(id);
            if (personaResponsable == null)
            {
                return NotFound();
            }

            _context.PersonasResponsables.Remove(personaResponsable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaResponsableExists(long id)
        {
            return _context.PersonasResponsables.Any(e => e.Id == id);
        }
    }
}
