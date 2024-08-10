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
        public async Task<IActionResult> IniciarSesion(string correo, string clave, string tipoUsuario)
        {
            if (tipoUsuario == "Usuario")
            {
                Usuario usuario = await _authenticationService.AuthenticateUsuarioAsync(correo, clave);
                if (usuario != null)
                {
                    var userRole = usuario.IdRolNavigation.NombreRol; // Ajusta esto según tu modelo

                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                        new Claim("UserId", usuario.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Role,userRole) // Agrega el rol a los claims


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

                    if (userRole == "Administrador")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (userRole == "Vendedor")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (userRole == "Operario")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else if (tipoUsuario == "Cliente")
            {
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

                    // Redirigir a Privacy si es Cliente
                    return RedirectToAction("Privacy", "Home");
                }
            }

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
