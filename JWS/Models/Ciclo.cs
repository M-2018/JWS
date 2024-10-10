using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JWS.Models
{
    public class Ciclo
    {
        [Key]
        public long Id { get; set; }
        public string Nombre { get; set; }

        [JsonIgnore]
        public ICollection<Estudiante> Estudiantes { get; set; }
        [JsonIgnore]
        public ICollection<Materia> Materias { get; set; }
    }
}
