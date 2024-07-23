using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Correousuario
    {
        public int IdCorreoUsuario { get; set; }
        public string CorreoUsuario { get; set; } = null!;
        public int IdUsuario { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
