using ContasApp.Data.Enums;

namespace ContasApp.Presentation.Models
{
    public class ContasConsultaResultadoViewModel
    {
        public  Guid? Id { get; set; }
        public string?  Nome { get; set; }
        public DateTime? Data { get; set; }
        public decimal? Valor { get; set; }
        public string? Categoria { get; set; }
        public string? Tipo { get; set; }
        public string? observacoes { get; set; }


    }
}
