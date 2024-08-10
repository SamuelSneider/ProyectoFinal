using Microsoft.EntityFrameworkCore;
using Motorcycle.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Motorcycle.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly MotorcycleContext _context;

        public UserAuthenticationService(MotorcycleContext context)
        {
            _context = context;
        }

        public async Task<Usuario> AuthenticateUsuarioAsync(string correo, string passwordu)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Correousuarios)
                .Include(u => u.IdRolNavigation) // Incluir la propiedad de navegación

                .FirstOrDefaultAsync(u => u.Correousuarios.Any(c => c.CorreoUsuario1 == correo) && u.PasswordUsuario == passwordu) ;

            // Manejar el caso en que `usuario` sea null
            if (usuario == null)
            {
                throw new InvalidOperationException("Usuario no encontrado o contraseña incorrecta.");
            }

            return usuario;
        }

        public async Task<Cliente> AuthenticateClienteAsync(string correo, string passwordc)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Correoclientes)
                .FirstOrDefaultAsync(c => c.Correoclientes.Any(co => co.CorreoCliente1 == correo) && c.PassWordCliente == passwordc);

            // Manejar el caso en que `cliente` sea null
            if (cliente == null)
            {
                throw new InvalidOperationException("Cliente no encontrado o contraseña incorrecta.");
            }

            return cliente;
        }
    }
}
