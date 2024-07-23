using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Cita = new HashSet<Citum>();
            Correousuarios = new HashSet<Correousuario>();
            Fichatecnicas = new HashSet<Fichatecnica>();
            Movilusuarios = new HashSet<Movilusuario>();
            Ordenventa = new HashSet<Ordenventum>();
            Productos = new HashSet<Producto>();
        }

        public int IdUsuario { get; set; }
        public string DocumentoUsuario { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string ApellidoUsuario { get; set; } = null!;
        public string PasswordUsuario { get; set; } = null!;
        public string GeneroUsuario { get; set; } = null!;
        public string EstadoUsuario { get; set; } = null!;
        public int IdRol { get; set; }

        public virtual Rol IdRolNavigation { get; set; } = null!;
        public virtual ICollection<Citum> Cita { get; set; }
        public virtual ICollection<Correousuario> Correousuarios { get; set; }
        public virtual ICollection<Fichatecnica> Fichatecnicas { get; set; }
        public virtual ICollection<Movilusuario> Movilusuarios { get; set; }
        public virtual ICollection<Ordenventum> Ordenventa { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
