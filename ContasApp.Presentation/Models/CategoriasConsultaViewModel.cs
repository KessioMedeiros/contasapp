using ContasApp.Data.Enums;

namespace ContasApp.Presentation.Models
{
    public class CategoriasConsultaViewModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public TipoCategoria? Tipo { get; set; }
    }
}
