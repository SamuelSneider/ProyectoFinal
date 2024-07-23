using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Movilusuario
    {
        public int IdMovilUsuario { get; set; }
        public string NumeroMovilUsuario { get; set; } = null!;
        public int IdUsuario { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
