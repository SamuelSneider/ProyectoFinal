using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Ordenventum
    {
        public Ordenventum()
        {
            Detalleventa = new HashSet<Detalleventa>();
            Ordentrabajos = new HashSet<Ordentrabajo>();
        }

        public int IdVenta { get; set; }
        public DateTime FechaOrdenVenta { get; set; }
        public decimal ValorTotalVenta { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstadoVenta { get; set; }
        public int IdCliente { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual Estadoventum IdEstadoVentaNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Detalleventa> Detalleventa { get; set; }
        public virtual ICollection<Ordentrabajo> Ordentrabajos { get; set; }
    }
}
