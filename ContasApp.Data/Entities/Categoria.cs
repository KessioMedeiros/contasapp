﻿using ContasApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Entities
{
    public class Categoria
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public TipoCategoria? Tipo { get; set; }
        public Guid? UsuarioId { get; set; }
        public List<Conta>? Contas { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
