using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Citum
    {
        public Citum()
        {
            Ordentrabajos = new HashSet<Ordentrabajo>();
        }

        public int IdCita { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        public DateTime FechaFinalizacionCita { get; set; }
        public int IdFichaTecnica { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual Fichatecnica IdFichaTecnicaNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Ordentrabajo> Ordentrabajos { get; set; }
    }
}
