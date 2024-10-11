namespace JWS.DTOs
{
    public class LoginResponseDTO
    {
        public bool IsValid { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
    }
}
