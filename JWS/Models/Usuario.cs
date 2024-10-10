using System.ComponentModel.DataAnnotations;

namespace JWS.Models
{
    public class Usuario
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        public bool IsAdmin { get; set; }
    }
}
