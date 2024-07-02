using System.ComponentModel.DataAnnotations;

namespace ContasApp.Presentation.Models
{
    public class AccountPasswordRecoverViewModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do usuário.")]
        public string? Email { get; set; }
    }
}
