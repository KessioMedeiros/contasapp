using System.ComponentModel.DataAnnotations;

namespace ContasApp.Presentation.Models
{
    public class AccountRegisterViewModel
    {
        [MinLength(8, ErrorMessage = "Por favor, informe no minimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no maximo {1} caracteres. ")]
        [Required(ErrorMessage = "Por favor, informe um nome de usuário.")]
        public string? Nome { get; set; }

        [EmailAddress (ErrorMessage = "Por favor, informe um email válido.")]
        [Required(ErrorMessage = "Por favor, informe um email válido.")]
        public string? Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",ErrorMessage = "Por favor, informe uma senha forte com no mínimo 8 caracteres.")]
        [Required(ErrorMessage = "Por favor, informe uma senha válida.")]
        public string? Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem, por favor verifique.")]
        [Required(ErrorMessage = "Por favor, confirme a senha de usuário.")]
        public string? SenhaConfirmacao { get; set; }
    }
}
