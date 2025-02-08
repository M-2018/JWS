using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JWS.Models
{
    public class Calificacion
    {
        [Key]
        public long Id { get; set; }

        [Range(0, 10)]
        public decimal? Taller { get; set; } //Taller

        [Range(0, 10)]
        public decimal? Trabajo { get; set; } //Trabajo

        [Range(0, 10)]
        public decimal? Exposicion { get; set; } //Expo

        [Range(0, 10)]
        public decimal? Tarea { get; set; } //Tarea

        [Range(0, 10)]
        public decimal? Quiz1 { get; set; } //quiz1

        [Range(0, 10)]
        public decimal? Quiz2 { get; set; } //quiz2

        [Range(0, 10)]
        public decimal? Actitudinal { get; set; } //Actitudinal

        [Range(0, 10)]
        public decimal? ExamenFinal { get; set; } //ExamFinal 

        [Range(0, 10)]
        public decimal? Definitiva { get; set; }

        public bool? Recuperacion { get; set; }

        [Range(0, 10)]
        public decimal? NotaRecuperacion { get; set; } //recuperacion  

        public bool? Habilitacion { get; set; }

        [Range(0, 10)]
        public decimal? NotaHabilitacion { get; set; } //habilitacion 


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