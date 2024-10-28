using JWS.Data;
using JWS.DTOs;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : Controller
    {
        private readonly APIAppDbContext _context;

        public EstudianteController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Estudiante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstudianteDTO>>> GetEstudiantes()
        {
            var estudiantes = await _context.Estudiantes
                .Include(e => e.Ciclo) // Incluir información del ciclo
                .ToListAsync();

            var estudiantesDTO = estudiantes.Select(e => new EstudianteDTO
            {
                Id = e.Id,
                Nombres = e.Nombres,
                Apellidos = e.Apellidos,
                NroDocumento = e.NroDocumento,
                TipoDocumento = e.TipoDocumento,
                FechaNacimiento = e.FechaNacimiento,
                Direccion = e.Direccion,
                Telefono = e.Telefono,
                Email = e.Email,
                SemestrePagado = e.SemestrePagado,
                CicloId = e.CicloId
            }).ToList();

            return Ok(estudiantesDTO);
        }

        // GET: api/Estudiante/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstudianteDTO>> GetEstudiante(long id)
        {
            var estudiante = await _context.Estudiantes
                .Include(e => e.Ciclo) // Incluir información del ciclo
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estudiante == null)
            {
                return NotFound();
            }

            var estudianteDTO = new EstudianteDTO
            {
                Id = estudiante.Id,
                Nombres = estudiante.Nombres,
                Apellidos = estudiante.Apellidos,
                NroDocumento = estudiante.NroDocumento,
                TipoDocumento = estudiante.TipoDocumento,
                FechaNacimiento = estudiante.FechaNacimiento,
                Direccion = estudiante.Direccion,
                Telefono = estudiante.Telefono,
                Email = estudiante.Email,
                SemestrePagado = estudiante.SemestrePagado,
                CicloId = estudiante.CicloId
            };

            return Ok(estudianteDTO);
        }

        // POST: api/Estudiante
        [HttpPost]
        public async Task<ActionResult<EstudianteDTO>> PostEstudiante(EstudianteDTO estudianteDTO)
        {
            var estudiante = new Estudiante
            {
                Nombres = estudianteDTO.Nombres,
                Apellidos = estudianteDTO.Apellidos,
                NroDocumento = estudianteDTO.NroDocumento,
                TipoDocumento = estudianteDTO.TipoDocumento,
                FechaNacimiento = estudianteDTO.FechaNacimiento,
                Direccion = estudianteDTO.Direccion,
                Telefono = estudianteDTO.Telefono,
                Email = estudianteDTO.Email,
                SemestrePagado = estudianteDTO.SemestrePagado,
                CicloId = estudianteDTO.CicloId
            };

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.Id }, estudianteDTO);
        }

        // PUT: api/Estudiante/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(long id, EstudianteDTO estudianteDTO)
        {
            if (id != estudianteDTO.Id)
            {
                return BadRequest();
            }

            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            estudiante.Nombres = estudianteDTO.Nombres;
            estudiante.Apellidos = estudianteDTO.Apellidos;
            estudiante.NroDocumento = estudianteDTO.NroDocumento;
            estudiante.TipoDocumento = estudianteDTO.TipoDocumento;
            estudiante.FechaNacimiento = estudianteDTO.FechaNacimiento;
            estudiante.Direccion = estudianteDTO.Direccion;
            estudiante.Telefono = estudianteDTO.Telefono;
            estudiante.Email = estudianteDTO.Email;
            estudiante.SemestrePagado = estudianteDTO.SemestrePagado;
            estudiante.CicloId = estudianteDTO.CicloId;

            _context.Entry(estudiante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Estudiante/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(long id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstudianteExists(long id)
        {
            return _context.Estudiantes.Any(e => e.Id == id);
        }
    }
}
