using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Detalleventa = new HashSet<Detalleventa>();
            Inventarios = new HashSet<Inventario>();
            OrdentrabajoServicioHasProductos = new HashSet<OrdentrabajoServicioHasProducto>();
        }

        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; } = null!;
        public string NombreProducto { get; set; } = null!;
        public string DescripcionProducto { get; set; } = null!;
        public byte[] FotoProducto { get; set; } = null!;
        public decimal ValorUnitarioProducto { get; set; }
        public int IdUsuario { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Detalleventa> Detalleventa { get; set; }
        public virtual ICollection<Inventario> Inventarios { get; set; }
        public virtual ICollection<OrdentrabajoServicioHasProducto> OrdentrabajoServicioHasProductos { get; set; }
    }
}
