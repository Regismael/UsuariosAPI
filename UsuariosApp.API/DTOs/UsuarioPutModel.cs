using System.ComponentModel.DataAnnotations;

namespace UsuariosApp.API.DTOs
{
    public class UsuarioPutModel
    {
        public Guid Id { get; set; }

        [MinLength(6, ErrorMessage = "O nome deve ter, no mínimo, 6 caracteres.")]
        [MaxLength(100, ErrorMessage = "O nome deve ter, no máximo, 100 caracteres.")]
        [Required(ErrorMessage = "O campo do nome é de preenchimento obrigatório.")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, insira um endereço de email válido")]
        [Required(ErrorMessage = "O campo do email é de preenchimento obrigatório.")]
        public string? Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()-_+=])[A-Za-z\d!@#$%^&*()-_+=]{8,}$",
            ErrorMessage = "A senha deve conter, pelo menos, 8 caracteres, 1 letra maiúscula, 1 minúscula, 1 dígito e 1 caractere especial")]
        [Required(ErrorMessage = "O campo da senha é de preenchimento obrigatório.")]
        public string? Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem, por favor verifique.")]
        [Required(ErrorMessage = "Por favor, confirme sua senha.")]
        public string? SenhaConfirmacao { get; set; }
    }
}
