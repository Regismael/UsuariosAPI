using System.ComponentModel.DataAnnotations;

namespace UsuariosApp.API.DTOs
{
    public class AutenticarUsuarioPostDTO
    {

        [EmailAddress(ErrorMessage = "Insira um formato de email válido.")]
        [Required(ErrorMessage = "O preenchimento do email é obrigatório.")]
        public string? Email { get; set; }

        [MinLength(8, ErrorMessage = "Senha inexistente, por favor, verifique. OBS: a senha deve conter, no mínimo, 8 caracteres.")]
        [Required(ErrorMessage = "O preenchimento da senha é obrigatório.")]
        public string? Senha { get; set; }
    }
}
