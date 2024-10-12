using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JWS.Models
{
    public class Calificacion
    {
        [Key]
        public long Id { get; set; }

        public decimal? NotaTrabajo1 { get; set; }
        public decimal? NotaTrabajo2 { get; set; }
        public decimal? NotaEvaluacion1 { get; set; }
        public decimal? NotaEvaluacion2 { get; set; }
        public decimal? NotaActitudinal { get; set; }
        public decimal? NotaExamenFinal { get; set; }
        public decimal? NotaDefinitiva { get; set; }
        public bool? Recuperacion { get; set; }
        public bool? Habilitacion { get; set; }

        public long EstudianteId { get; set; }
        [JsonIgnore]
        public Estudiante Estudiante { get; set; }

        public long CicloId { get; set; }
        [JsonIgnore]
        public Ciclo Ciclo { get; set; }

        // Nueva relación con Materia
        public long MateriaId { get; set; }
        [JsonIgnore]
        public Materia Materia { get; set; }
    }
}