using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Detalleventa
    {
        public int IdDetalleVentas { get; set; }
        public int CantidaDetalleVentas { get; set; }
        public decimal ValorTotalDetalleVentas { get; set; }
        public decimal ValorUnitarioDetalleVentas { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual Ordenventum IdVentaNavigation { get; set; } = null!;
    }
}
