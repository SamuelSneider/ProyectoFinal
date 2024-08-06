using System;
using System.Collections.Generic;

namespace YourNamespace.ViewModels
{
    public class VentaViewModel
    {
        public int IdDetalleVentas { get; set; }
        public DateTime FechaVenta { get; set; }
        public List<DetalleVentaViewModel> DetalleVentas { get; set; }
        public IEnumerable<object> Detalleventa { get; internal set; }
    }
    public class DetalleVentaViewModel
    {
        public int IdProducto { get; set; }
        public int CantidaDetalleVentas { get; set; }
    }
}

