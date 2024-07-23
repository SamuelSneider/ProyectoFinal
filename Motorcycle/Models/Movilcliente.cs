using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Movilcliente
    {
        public int IdMovilCliente { get; set; }
        public string NumeroMovilCliente { get; set; } = null!;
        public int IdCliente { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
    }
}
