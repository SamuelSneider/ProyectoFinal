using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Motorcycle.Models;
using Motorcycle.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Motorcycle.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserAuthenticationService _authenticationService;

        public LoginController(IUserAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            // Primero, intentamos autenticar como Usuario
            Usuario usuario = await _authenticationService.AuthenticateUsuarioAsync(correo, clave);
            if (usuario != null)
            {
                var userRole = usuario.IdRolNavigation.NombreRol;

                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                    new Claim("UserId", usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, userRole)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties
                {
                    AllowRefresh = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    properties
                );

                return RedirectToAction("Index", "Home");
            }

            // Si no es Usuario, intentamos autenticar como Cliente
            Cliente cliente = await _authenticationService.AuthenticateClienteAsync(correo, clave);
            if (cliente != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, cliente.NombreCliente),
                    new Claim("ClientId", cliente.IdCliente.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties
                {
                    AllowRefresh = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    properties
                );

                return RedirectToAction("Privacy", "Home");
            }

            // Si no es ni Usuario ni Cliente, mostramos un mensaje de error
            ViewData["Mensaje"] = "Usuario o contraseña incorrectos";
            return View();
        }

        // Método para cerrar sesión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSesion", "Login");
        }
    }
}
