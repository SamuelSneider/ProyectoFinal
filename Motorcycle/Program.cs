using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Motorcycle.Models;
using Motorcycle.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuración de la base de datos
builder.Services.AddDbContext<MotorcycleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));



// Configuración de la autenticación de cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/IniciarSesion"; // Ruta para la página de inicio de sesión
        options.LogoutPath = "/Login/CerrarSesion"; // Ruta para cerrar sesión
        options.AccessDeniedPath = "/Account/AccessDenied"; // Ruta para el acceso denegado
    });

// Registro del servicio de autenticación de usuario
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

// Registro del servicio de correo electrónico
builder.Services.AddScoped<EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Añadir autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=IniciarSesion}/{id?}");


app.Run();
