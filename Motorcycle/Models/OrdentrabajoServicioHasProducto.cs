using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class OrdentrabajoServicioHasProducto
    {
        public int IdOrdentrabajoServicioHasProductos { get; set; }
        public int IdServicioOrdenTrabajo { get; set; }
        public int IdProducto { get; set; }
        public decimal PrecioProductoOrdentrabajoServicioHasProductos { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual ServicioHasOrdentrabajo IdServicioOrdenTrabajoNavigation { get; set; } = null!;
    }
}
