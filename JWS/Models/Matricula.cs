using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JWS.Models
{
    public class Matricula
    {
        [Key]
        public long Id { get; set; }

        public long EstudianteId { get; set; }
        [JsonIgnore]
        public Estudiante Estudiante { get; set; }

        public long MateriaId { get; set; }
        [JsonIgnore]
        public Materia Materia { get; set; }

        public int AnioLectivo { get; set; }

        [JsonIgnore]
        public ICollection<Calificacion> Calificaciones { get; set; }
    }
}
