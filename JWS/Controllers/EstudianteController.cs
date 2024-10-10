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

        // GET: api/estudiantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstudianteDTO>>> GetEstudiantes()
        {
            return await _context.Estudiantes
                .Select(e => new EstudianteDTO
                {
                    Id = e.Id,
                    CodigoUnico = e.CodigoUnico,
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
                })
                .ToListAsync();
        }

        // GET: api/estudiantes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EstudianteDTO>> GetEstudiante(long id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            var estudianteDTO = new EstudianteDTO
            {
                Id = estudiante.Id,
                CodigoUnico = estudiante.CodigoUnico,
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

            return estudianteDTO;
        }

        // POST: api/estudiantes
        [HttpPost]
        public async Task<ActionResult<EstudianteDTO>> PostEstudiante(EstudianteDTO estudianteDTO)
        {
            var estudiante = new Estudiante
            {
                CodigoUnico = estudianteDTO.CodigoUnico,
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

            estudianteDTO.Id = estudiante.Id;

            return CreatedAtAction(nameof(GetEstudiante), new { id = estudianteDTO.Id }, estudianteDTO);
        }

        // PUT: api/estudiantes/{id}
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

            estudiante.CodigoUnico = estudianteDTO.CodigoUnico;
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
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/estudiantes/{id}
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
