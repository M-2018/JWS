﻿namespace JWS.DTOs
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
        public bool? Habilitacion { get; set; }
        public long MatriculaId { get; set; }
        public long MateriaId { get; set; }
        public long ProfesorId { get; set; }
    }
}
