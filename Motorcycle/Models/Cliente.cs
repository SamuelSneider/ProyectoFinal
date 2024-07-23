using System;
using System.Collections.Generic;

namespace Motorcycle.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cita = new HashSet<Citum>();
            Correoclientes = new HashSet<Correocliente>();
            Fichatecnicas = new HashSet<Fichatecnica>();
            Movilclientes = new HashSet<Movilcliente>();
            Ordenventa = new HashSet<Ordenventum>();
        }

        public int IdCliente { get; set; }
        public string DocumentoCliente { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;
        public string ApellidoCliente { get; set; } = null!;
        public string GeneroCliente { get; set; } = null!;
        public string? EstadoCliente { get; set; }
        public string? PassWordCliente { get; set; }
        public DateTime RegistroCliente { get; set; }

        public virtual ICollection<Citum> Cita { get; set; }
        public virtual ICollection<Correocliente> Correoclientes { get; set; }
        public virtual ICollection<Fichatecnica> Fichatecnicas { get; set; }
        public virtual ICollection<Movilcliente> Movilclientes { get; set; }
        public virtual ICollection<Ordenventum> Ordenventa { get; set; }
    }
}
