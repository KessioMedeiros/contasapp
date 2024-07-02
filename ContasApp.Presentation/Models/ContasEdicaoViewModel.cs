using System.ComponentModel.DataAnnotations;

namespace ContasApp.Presentation.Models
{
    public class ContasEdicaoViewModel
    {
        public Guid? Id { get; set; }

        [MaxLength(100, ErrorMessage = "Por favor, informe um máximo de {1} caracteres")]
        [MinLength(8, ErrorMessage = "Por favor, informe um minimo de {1} caracteres")]
        [Required(ErrorMessage = "Por favor, informe o nome da categoria")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data da categorias")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Por favor, informe o valor da categoria")]
        public decimal? Valor { get; set; }

        [MaxLength(250, ErrorMessage = "Por favor, informe um máximo de {1} caracteres")]
        [MinLength(6, ErrorMessage = "Por favor, informe um minimo de {1} caracteres")]
        [Required(ErrorMessage = "Por favor, informe uma observação")]
        public string? Observacoes { get; set; }

        [Required(ErrorMessage = "Por favor, informe o id da categoria")]
        public Guid? CategoriaId { get; set; }
    }
}
