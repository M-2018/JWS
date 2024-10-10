namespace JWS.DTOs
{
    public class AsistenciaDTO
    {
        public long Id { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Presente { get; set; }
        public long EstudianteId { get; set; }
        public long MateriaId { get; set; }
    }
}
