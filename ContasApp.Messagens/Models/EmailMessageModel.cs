using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Messagens.Models
{
    public class EmailMessageModel
    {
        public string? EmailDestinatario { get; set; }
        public string? Assunto { get; set; }
        public string?  Corpo { get; set; }
    }
}
