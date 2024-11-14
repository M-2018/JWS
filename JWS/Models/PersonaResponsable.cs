using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JWS.Models
{
    public class PersonaResponsable
    {
        [Key]
        public long Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Relacion { get; set; }

        public long EstudianteId { get; set; }
        [JsonIgnore]
        public Estudiante Estudiante { get; set; }

        [EmailAddress] 
        public string CorreoElectronico { get; set; }

        public string Telefono { get; set; }
    }
}
