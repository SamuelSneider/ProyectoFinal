using Motorcycle.Models;

namespace Motorcycle.Services
{
    public interface IUserAuthenticationService
    {
        Task<Usuario> AuthenticateUsuarioAsync(string correo, string passwordu);
        Task<Cliente> AuthenticateClienteAsync(string correo, string passwordc);

    }
}
