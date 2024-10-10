namespace JWS.DTOs
{
    public class ProfesorDTO
    {
        public long Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Especialidad { get; set; }
        public bool Activo { get; set; }
    }
}
