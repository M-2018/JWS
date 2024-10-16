using JWS.Data;
using JWS.DTOs;
using JWS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly APIAppDbContext _context;

        public UsuarioController(APIAppDbContext context)
        {
            _context = context;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            return await _context.Usuarios
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Nombres = u.Nombres,
                    Apellidos = u.Apellidos,
                    IsAdmin = u.IsAdmin
                })
                .ToListAsync();
        }

        // GET: api/usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioDTO = new UsuarioDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                IsAdmin = usuario.IsAdmin
            };

            return usuarioDTO;
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> PostUsuario(UsuarioCreateDTO usuarioCreateDTO)
        {
            var hashedPassword = PasswordHasher.HashPassword(usuarioCreateDTO.Password);

            var usuario = new Usuario
            {
                Username = usuarioCreateDTO.Username,
                Email = usuarioCreateDTO.Email,
                Password = hashedPassword,
                Nombres = usuarioCreateDTO.Nombres,
                Apellidos = usuarioCreateDTO.Apellidos,
                IsAdmin = usuarioCreateDTO.IsAdmin
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var usuarioDTO = new UsuarioDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                IsAdmin = usuario.IsAdmin
            };

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuarioDTO);
        }


        // PUT: api/usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(long id, UsuarioDTO usuarioDTO)
        {
            if (id != usuarioDTO.Id)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Username = usuarioDTO.Username;
            usuario.Email = usuarioDTO.Email;
            usuario.Nombres = usuarioDTO.Nombres;
            usuario.Apellidos = usuarioDTO.Apellidos;
            usuario.IsAdmin = usuarioDTO.IsAdmin;

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        [HttpPost("validate-credentials")]
        public async Task<ActionResult<LoginResponseDTO>> ValidateUserCredentials(LoginDTO loginDTO)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

            if (usuario == null || !VerifyPassword(loginDTO.Password, usuario.Password))
            {
                return new LoginResponseDTO { IsValid = false, Username = null, IsAdmin = false };
            }

            return new LoginResponseDTO { IsValid = true, Username = usuario.Username, IsAdmin = usuario.IsAdmin };
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            return PasswordHasher.VerifyPassword(inputPassword, storedHash);
        }





        // DELETE: api/usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(long id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
