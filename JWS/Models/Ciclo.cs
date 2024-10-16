using JWS.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JWS.Models
{    
    public class Ciclo
    {
        [Key]
        public long Id { get; set; }

        public string Nombre { get; set; }

        [Range(2000, 2100, ErrorMessage = "El año debe estar entre 2000 y 2100.")]
        public string Anio { get; set; }

        [Range(1, 2, ErrorMessage = "El valor del semestre debe ser 1 (Primer Semestre) o 2 (Segundo Semestre).")]
        public string Semestre { get; set; }

        [JsonIgnore]
        public ICollection<Estudiante> Estudiantes { get; set; }

        [JsonIgnore]
        public ICollection<Materia> Materias { get; set; }
    }
}
