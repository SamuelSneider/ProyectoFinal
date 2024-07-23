using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Inventario
    {
        public int IdInventario { get; set; }
        public int CantidadInventario { get; set; }
        public DateTime? FechaMovimientoInventario { get; set; }
        public int IdProducto { get; set; }
        public decimal? ValorUnitarioInventario { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
