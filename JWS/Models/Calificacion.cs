using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JWS.Models
{
    public class Calificacion
    {
        [Key]
        public long Id { get; set; }

        [Range(0, 10)]
        public decimal? NotaTrabajo1 { get; set; }

        [Range(0, 10)]
        public decimal? NotaTrabajo2 { get; set; }

        [Range(0, 10)]
        public decimal? NotaEvaluacion1 { get; set; }

        [Range(0, 10)]
        public decimal? NotaEvaluacion2 { get; set; }

        [Range(0, 10)]
        public decimal? NotaActitudinal { get; set; }

        [Range(0, 10)]
        public decimal? NotaExamenFinal { get; set; }

        [Range(0, 10)]
        public decimal? NotaDefinitiva { get; set; }

        public bool? Recuperacion { get; set; }

        [Range(0, 10)]
        public decimal? NotaRecuperacion { get; set; }

        public bool? Habilitacion { get; set; }

        [Range(0, 10)]
        public decimal? NotaHabilitacion { get; set; }


        public long EstudianteId { get; set; }

        [JsonIgnore]
        public Estudiante Estudiante { get; set; }

        public long CicloId { get; set; }

        [JsonIgnore]
        public Ciclo Ciclo { get; set; }

        public long MateriaId { get; set; }
        [JsonIgnore]
        public Materia Materia { get; set; }
    }
}