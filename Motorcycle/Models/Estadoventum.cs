using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Estadoventum
    {
        public Estadoventum()
        {
            Ordenventa = new HashSet<Ordenventum>();
        }

        public int IdEstadoVenta { get; set; }
        public string NombreEstadoVenta { get; set; } = null!;

        public virtual ICollection<Ordenventum> Ordenventa { get; set; }
    }
}
