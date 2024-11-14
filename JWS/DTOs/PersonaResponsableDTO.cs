namespace JWS.DTOs
{
    public class PersonaResponsableDTO
    {
        public long Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Relacion { get; set; }
        public long EstudianteId { get; set; }

        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
