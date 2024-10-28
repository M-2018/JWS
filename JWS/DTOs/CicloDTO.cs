namespace JWS.DTOs
{
    public class CicloDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Anio { get; set; }
        public string Semestre { get; set; }

        public List<long> MateriasIds { get; set; }
    }
}
