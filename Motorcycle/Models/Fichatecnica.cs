using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Fichatecnica
    {
        public Fichatecnica()
        {
            Cita = new HashSet<Citum>();
        }

        public int IdFichaTecnica { get; set; }
        public DateTime FechaRegistroFichaTecnica { get; set; }
        public string NumeroMotorFichaTecnica { get; set; } = null!;
        public string CilindrajeFichaTecnica { get; set; } = null!;
        public string PlacaFichaTecnica { get; set; } = null!;
        public string MarcaFichaTecnica { get; set; } = null!;
        public string ChacisFichaTecnica { get; set; } = null!;
        public string ModeloFichaTecnica { get; set; } = null!;
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Citum> Cita { get; set; }
    }
}
