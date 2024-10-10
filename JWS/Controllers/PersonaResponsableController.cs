using JWS.Data;
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

        // GET: api/personasresponsables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaResponsable>>> GetPersonasResponsables()
        {
            return await _context.PersonasResponsables.ToListAsync();
        }

        // GET: api/personasresponsables/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaResponsable>> GetPersonaResponsable(long id)
        {
            var personaResponsable = await _context.PersonasResponsables.FindAsync(id);

            if (personaResponsable == null)
            {
                return NotFound();
            }

            return personaResponsable;
        }

        // POST: api/personasresponsables
        [HttpPost]
        public async Task<ActionResult<PersonaResponsable>> PostPersonaResponsable(PersonaResponsable personaResponsable)
        {
            _context.PersonasResponsables.Add(personaResponsable);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersonaResponsable), new { id = personaResponsable.Id }, personaResponsable);
        }

        // PUT: api/personasresponsables/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaResponsable(long id, PersonaResponsable personaResponsable)
        {
            if (id != personaResponsable.Id)
            {
                return BadRequest();
            }

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
