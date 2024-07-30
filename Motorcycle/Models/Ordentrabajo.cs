using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Ordentrabajo
    {
        public Ordentrabajo()
        {
            ServicioHasOrdentrabajos = new HashSet<ServicioHasOrdentrabajo>();
        }

        public int IdOrdenTrabajo { get; set; }
        public decimal? NumeroOrdenTrabajo { get; set; }
        public int IdCita { get; set; }
        public int IdVenta { get; set; }
        public virtual Citum IdCitaNavigation { get; set; } = null!;
        public virtual Ordenventum IdVentaNavigation { get; set; } = null!;
        public virtual ICollection<ServicioHasOrdentrabajo> ServicioHasOrdentrabajos { get; set; }
    }
}
