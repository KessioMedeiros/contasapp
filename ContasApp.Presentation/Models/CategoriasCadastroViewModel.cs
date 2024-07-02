using ContasApp.Data.Enums;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ContasApp.Presentation.Models
{
    public class CategoriasCadastroViewModel
    {
        [MaxLength(100, ErrorMessage = "Por favor, informe um máximo de {1} caracteres" )]
        [MinLength(8, ErrorMessage = "Por favor, informe um minimo de {1} caracteres") ]
        [Required(ErrorMessage = "Por favor, informe o nome da categoria")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Por favor, selecione o tipo da categoria.")]
        public TipoCategoria? Tipo { get; set; }
    }
}
