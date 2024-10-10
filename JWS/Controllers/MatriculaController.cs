using JWS.Data;
using JWS.DTOs;
using JWS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaController : Controller
    {
        private readonly APIAppDbContext _context;

        public MatriculaController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/matriculas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatriculaDTO>>> GetMatriculas()
        {
            return await _context.Matriculas
                .Select(m => new MatriculaDTO
                {
                    Id = m.Id,
                    EstudianteId = m.EstudianteId,
                    MateriaId = m.MateriaId,
                    AnioLectivo = m.AnioLectivo
                })
                .ToListAsync();
        }

        // GET: api/matriculas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MatriculaDTO>> GetMatricula(long id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);

            if (matricula == null)
            {
                return NotFound();
            }

            var matriculaDTO = new MatriculaDTO
            {
                Id = matricula.Id,
                EstudianteId = matricula.EstudianteId,
                MateriaId = matricula.MateriaId,
                AnioLectivo = matricula.AnioLectivo
            };

            return matriculaDTO;
        }

        // POST: api/matriculas
        [HttpPost]
        public async Task<ActionResult<MatriculaDTO>> PostMatricula(MatriculaDTO matriculaDTO)
        {
            var matricula = new Matricula
            {
                EstudianteId = matriculaDTO.EstudianteId,
                MateriaId = matriculaDTO.MateriaId,
                AnioLectivo = matriculaDTO.AnioLectivo
            };

            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();

            matriculaDTO.Id = matricula.Id;

            return CreatedAtAction(nameof(GetMatricula), new { id = matriculaDTO.Id }, matriculaDTO);
        }

        // PUT: api/matriculas/{id}
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
            matricula.MateriaId = matriculaDTO.MateriaId;
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
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/matriculas/{id}
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
            return _context.Matriculas.Any(e => e.Id == id);
        }
    }
}
