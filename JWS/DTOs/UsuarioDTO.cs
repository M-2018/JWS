namespace JWS.DTOs
{
    public class UsuarioDTO
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public bool IsAdmin { get; set; }
    }
}
