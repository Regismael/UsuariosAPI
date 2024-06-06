namespace UsuariosApp.API.DTOs
{
    public class AutenticarUsuarioResponseDTO
    {
        public Guid? Id { get; set; }
        public string? AccessToken { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }
}
