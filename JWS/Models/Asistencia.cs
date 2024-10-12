using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JWS.Models
{
    public class Asistencia
    {
        [Key]
        public long Id { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Presente { get; set; }
        public bool? Ausente { get; set; }

        public long EstudianteId { get; set; }
        [JsonIgnore]
        public Estudiante Estudiante { get; set; }

        public long MateriaId { get; set; }
        [JsonIgnore]
        public Materia Materia { get; set; }

        public long CicloId { get; set; }
        [JsonIgnore]
        public Ciclo Ciclo { get; set; }
    }
}
