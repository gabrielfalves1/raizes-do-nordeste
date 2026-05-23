using System.ComponentModel.DataAnnotations;

namespace raizes_do_nordeste.DTOs
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome não pode exceder 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [StringLength(150, ErrorMessage = "O e-mail não pode exceder 150 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre {2} e {1} caracteres.")]
        public string Senha { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "O telefone não pode exceder 20 caracteres.")]
        public string Telefone { get; set; } = string.Empty;
    }
}