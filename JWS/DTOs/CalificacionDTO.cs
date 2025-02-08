namespace JWS.DTOs
{
    public class CalificacionDTO
    {
        public long Id { get; set; }
        public decimal? Taller { get; set; }
        public decimal? Trabajo { get; set; }
        public decimal? Exposicion { get; set; }
        public decimal? Tarea { get; set; }
        public decimal? Quiz1 { get; set; }
        public decimal? Quiz2 { get; set; }
        public decimal? Actitudinal { get; set; }
        public decimal? ExamenFinal { get; set; }
        public decimal? Definitiva { get; set; }
        public bool? Recuperacion { get; set; }
        public decimal? NotaRecuperacion { get; set; }
        public bool? Habilitacion { get; set; }
        public decimal? NotaHabilitacion { get; set; }
        public long EstudianteId { get; set; }
        public long CicloId { get; set; }
        public long MateriaId { get; set; }
    }
}
