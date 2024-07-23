using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Servicio
    {
        public Servicio()
        {
            ServicioHasOrdentrabajos = new HashSet<ServicioHasOrdentrabajo>();
        }

        public int IdServicio { get; set; }
        public string NombreServicio { get; set; } = null!;
        public decimal PrecioManoObraServicio { get; set; }

        public virtual ICollection<ServicioHasOrdentrabajo> ServicioHasOrdentrabajos { get; set; }
    }
}
