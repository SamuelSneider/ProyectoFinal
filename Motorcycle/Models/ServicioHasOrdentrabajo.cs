using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class ServicioHasOrdentrabajo
    {
        public ServicioHasOrdentrabajo()
        {
            OrdentrabajoServicioHasProductos = new HashSet<OrdentrabajoServicioHasProducto>();
        }

        public int IdServicioOrdenTrabajo { get; set; }
        public int IdServicio { get; set; }
        public int IdOrdenTrabajo { get; set; }
        public decimal PrecioServicioHasOrdenTrabajo { get; set; }

        public virtual Ordentrabajo IdOrdenTrabajoNavigation { get; set; } = null!;
        public virtual Servicio IdServicioNavigation { get; set; } = null!;
        public virtual ICollection<OrdentrabajoServicioHasProducto> OrdentrabajoServicioHasProductos { get; set; }
    }
}
