using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Correocliente
    {
        public int IdCorreoCliente { get; set; }
        public string CorreoCliente1 { get; set; } = null!;
        public int IdCliente { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
    }
}
