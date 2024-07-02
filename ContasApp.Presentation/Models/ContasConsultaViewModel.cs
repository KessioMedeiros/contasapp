using System.ComponentModel.DataAnnotations;

namespace ContasApp.Presentation.Models
{
    public class ContasConsultaViewModel
    {
        [Required(ErrorMessage = "Por favor, informe uma data de início" )]
        public DateTime? DataInicio { get; set; }

        [Required(ErrorMessage = "Por favor, informe uma data de fim")]
        public DateTime? DataFim { get; set; }

        public List<ContasConsultaResultadoViewModel>? Resultado { get; set; }
    }
}
