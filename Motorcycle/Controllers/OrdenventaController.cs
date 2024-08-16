using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Motorcycle.Models;
using Motorcycle.ViewModel;

namespace Motorcycle.Controllers
{
    public class OrdenventaController : Controller
    {
        private readonly MotorcycleContext _context;
        private const int PageSize = 10; // Tamaño de página para la paginación
        static List<ProductoCarrito> productosCarrito = new List<ProductoCarrito>() { };
        public OrdenventaController(MotorcycleContext context)
        {
            _context = context;
        }

        // GET: Ordenventa
        public async Task<IActionResult> Index(string buscar, int page = 1)
        {
            var query = _context.Ordenventa
                .Include(o => o.IdClienteNavigation)
                .Include(o => o.IdEstadoVentaNavigation)
                .Include(o => o.IdUsuarioNavigation)
                .AsQueryable();

            // Búsqueda
            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(o =>
                    o.IdClienteNavigation.NombreCliente.Contains(buscar) ||
                    o.IdUsuarioNavigation.NombreUsuario.Contains(buscar) ||
                    o.IdEstadoVentaNavigation.IdEstadoVenta.ToString().Contains(buscar));
            }

            // Paginación
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
            var paginatedItems = await query
                .OrderBy(o => o.IdVenta)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["SearchQuery"] = buscar;

            return View(paginatedItems);
        }

        // GET: Ordenventa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ordenventa == null)
            {
                return NotFound();
            }

            var ordenventum = await _context.Ordenventa
                .Include(o => o.IdClienteNavigation)
                .Include(o => o.IdEstadoVentaNavigation)
                .Include(o => o.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (ordenventum == null)
            {
                return NotFound();
            }

            return View(ordenventum);
        }

        // GET: Ordenventa/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente");
            ViewData["IdEstadoVenta"] = new SelectList(_context.Estadoventa, "IdEstadoVenta", "IdEstadoVenta");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View();
        }

        // POST: Ordenventa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenta,FechaOrdenVenta,ValorTotalVenta,IdUsuario,IdEstadoVenta,IdCliente")] Ordenventum ordenventum)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(ordenventum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", ordenventum.IdCliente);
            ViewData["IdEstadoVenta"] = new SelectList(_context.Estadoventa, "IdEstadoVenta", "IdEstadoVenta", ordenventum.IdEstadoVenta);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", ordenventum.IdUsuario);
            return View(ordenventum);
        }

        // GET: Ordenventa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ordenventa == null)
            {
                return NotFound();
            }

            var ordenventum = await _context.Ordenventa.FindAsync(id);
            if (ordenventum == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", ordenventum.IdCliente);
            ViewData["IdEstadoVenta"] = new SelectList(_context.Estadoventa, "IdEstadoVenta", "IdEstadoVenta", ordenventum.IdEstadoVenta);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", ordenventum.IdUsuario);
            return View(ordenventum);
        }

        // POST: Ordenventa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenta,FechaOrdenVenta,ValorTotalVenta,IdUsuario,IdEstadoVenta,IdCliente")] Ordenventum ordenventum)
        {
            if (id != ordenventum.IdVenta)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenventum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenventumExists(ordenventum.IdVenta))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", ordenventum.IdCliente);
            ViewData["IdEstadoVenta"] = new SelectList(_context.Estadoventa, "IdEstadoVenta", "IdEstadoVenta", ordenventum.IdEstadoVenta);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", ordenventum.IdUsuario);
            return View(ordenventum);
        }

        // GET: Ordenventa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ordenventa == null)
            {
                return NotFound();
            }

            var ordenventum = await _context.Ordenventa
                .Include(o => o.IdClienteNavigation)
                .Include(o => o.IdEstadoVentaNavigation)
                .Include(o => o.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (ordenventum == null)
            {
                return NotFound();
            }

            return View(ordenventum);
        }

        // POST: Ordenventa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ordenventa == null)
            {
                return Problem("Entity set 'MotorcycleContext.Ordenventa'  is null.");
            }
            var ordenventum = await _context.Ordenventa.FindAsync(id);
            if (ordenventum != null)
            {
                _context.Ordenventa.Remove(ordenventum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenventumExists(int id)
        {
            return (_context.Ordenventa?.Any(e => e.IdVenta == id)).GetValueOrDefault();
        }

      
        public async Task<IActionResult> RegistrarVenta()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "NombreProducto");
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente");
            productosCarrito = new List<ProductoCarrito>();
            return View(new RegistrarVentaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVenta(RegistrarVentaViewModel venta)
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "NombreProducto");
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente");
            var productos = _context.Productos.Where(x => x.IdProducto == venta.IdProducto).FirstOrDefault();
            productosCarrito.Add(new ProductoCarrito() { IdCliente = venta.IdCliente,IdProducto = venta.IdProducto,NombreProducto = productos.NombreProducto, Cantidad = venta.Cantidad,Valor=productos.ValorUnitarioProducto});
            venta.Productos = productosCarrito;
            return View(venta);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarOrdenVenta()
        {
            var ordenVenta = new Ordenventum()
            {
                IdEstadoVenta = 1,
                IdCliente = productosCarrito.FirstOrDefault().IdCliente,
                IdUsuario = 1,
                FechaOrdenVenta = DateTime.Now,
                ValorTotalVenta = 0
            };

            _context.Ordenventa.Add(ordenVenta);
            await _context.SaveChangesAsync();
            var detalleVentas = productosCarrito.Select(x => new Detalleventa()
            {
                IdVenta = ordenVenta.IdVenta,
                IdProducto = x.IdProducto,
                CantidaDetalleVentas = x.Cantidad,
                ValorUnitarioDetalleVentas = x.Valor,
                ValorTotalDetalleVentas = 0
            });
            _context.Detalleventas.AddRange(detalleVentas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
