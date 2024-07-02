using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Entities
{
    public class Conta
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public DateTime? Data { get; set; }
        public decimal? Valor { get; set; }
        public string? Observacoes { get; set; }
        public Guid? CategoriaId { get; set; }
        public Guid? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
