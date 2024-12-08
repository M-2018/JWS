namespace JWS.DTOs
{
    public class CalificacionDTO
    {
        public long Id { get; set; }
        public decimal? NotaTrabajo1 { get; set; }
        public decimal? NotaTrabajo2 { get; set; }
        public decimal? NotaEvaluacion1 { get; set; }
        public decimal? NotaEvaluacion2 { get; set; }
        public decimal? NotaActitudinal { get; set; }
        public decimal? NotaExamenFinal { get; set; }
        public decimal? NotaDefinitiva { get; set; }
        public bool? Recuperacion { get; set; }
        public decimal? NotaRecuperacion { get; set; }
        public bool? Habilitacion { get; set; }
        public decimal? NotaHabilitacion { get; set; }
        public long EstudianteId { get; set; }
        public long CicloId { get; set; }
        public long MateriaId { get; set; }
    }
}
