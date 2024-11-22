using JWS.Data;
using JWS.DTOs;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaController : ControllerBase
    {
        private readonly APIAppDbContext _context;

        public MatriculaController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Matricula
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatriculaDTO>>> GetMatriculas()
        {
            var matriculas = await _context.Matriculas
                .Include(m => m.Estudiante) // Incluir información del estudiante
                .Include(m => m.Ciclo) // Incluir información del ciclo
                .ToListAsync();

            var matriculasDTO = matriculas.Select(m => new MatriculaDTO
            {
                Id = m.Id,
                EstudianteId = m.EstudianteId,
                CicloId = m.CicloId,
                AnioLectivo = m.AnioLectivo
            }).ToList();

            return Ok(matriculasDTO);
        }

        // GET: api/Matricula/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MatriculaDTO>> GetMatricula(long id)
        {
            var matricula = await _context.Matriculas
                .Include(m => m.Estudiante) // Incluir información del estudiante
                .Include(m => m.Ciclo) // Incluir información del ciclo
                .FirstOrDefaultAsync(m => m.Id == id);

            if (matricula == null)
            {
                return NotFound();
            }

            var matriculaDTO = new MatriculaDTO
            {
                Id = matricula.Id,
                EstudianteId = matricula.EstudianteId,
                CicloId = matricula.CicloId,
                AnioLectivo = matricula.AnioLectivo
            };

            return Ok(matriculaDTO);
        }

        // POST: api/Matricula
        [HttpPost]
        public async Task<ActionResult<MatriculaDTO>> PostMatricula(MatriculaDTO matriculaDTO)
        {
            var matricula = new Matricula
            {
                EstudianteId = matriculaDTO.EstudianteId,
                CicloId = matriculaDTO.CicloId, // Usamos CicloId
                AnioLectivo = matriculaDTO.AnioLectivo
            };

            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMatricula), new { id = matricula.Id }, matriculaDTO);
        }

        // PUT: api/Matricula/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatricula(long id, MatriculaDTO matriculaDTO)
        {
            if (id != matriculaDTO.Id)
            {
                return BadRequest();
            }

            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }

            matricula.EstudianteId = matriculaDTO.EstudianteId;
            matricula.CicloId = matriculaDTO.CicloId; // Actualizamos CicloId
            matricula.AnioLectivo = matriculaDTO.AnioLectivo;

            _context.Entry(matricula).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatriculaExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Matricula/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatricula(long id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatriculaExists(long id)
        {
            return _context.Matriculas.Any(m => m.Id == id);
        }

    }
}
