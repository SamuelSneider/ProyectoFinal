using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Motorcycle.Models;

namespace Motorcycle.Controllers
{
    public class OrdentrabajoServicioHasProductoController : Controller
    {
        private readonly MotorcycleContext _context;
        private const int PageSize = 10; // Tamaño de página para la paginación

        public OrdentrabajoServicioHasProductoController(MotorcycleContext context)
        {
            _context = context;
        }

        // GET: OrdentrabajoServicioHasProducto
        public async Task<IActionResult> Index(string buscar, int page = 1)
        {
            var query = _context.OrdentrabajoServicioHasProductos
                .Include(o => o.IdProductoNavigation)
                .Include(o => o.IdServicioOrdenTrabajoNavigation)
                .AsQueryable();

            // Búsqueda
            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(o =>
                    o.IdProductoNavigation.IdProducto.ToString().Contains(buscar) ||
                    o.IdServicioOrdenTrabajoNavigation.IdServicioOrdenTrabajo.ToString().Contains(buscar));
            }

            // Paginación
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
            var paginatedItems = await query
                .OrderBy(o => o.IdOrdentrabajoServicioHasProductos)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["SearchQuery"] = buscar;

            return View(paginatedItems);
        }

        // GET: OrdentrabajoServicioHasProducto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrdentrabajoServicioHasProductos == null)
            {
                return NotFound();
            }

            var ordentrabajoServicioHasProducto = await _context.OrdentrabajoServicioHasProductos
                .Include(o => o.IdProductoNavigation)
                .Include(o => o.IdServicioOrdenTrabajoNavigation)
                .FirstOrDefaultAsync(m => m.IdOrdentrabajoServicioHasProductos == id);
            if (ordentrabajoServicioHasProducto == null)
            {
                return NotFound();
            }

            return View(ordentrabajoServicioHasProducto);
        }

        // GET: OrdentrabajoServicioHasProducto/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            ViewData["IdServicioOrdenTrabajo"] = new SelectList(_context.ServicioHasOrdentrabajos, "IdServicioOrdenTrabajo", "IdServicioOrdenTrabajo");
            return View();
        }

        // POST: OrdentrabajoServicioHasProducto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrdentrabajoServicioHasProductos,IdServicioOrdenTrabajo,IdProducto,PrecioProductoOrdentrabajoServicioHasProductos")] OrdentrabajoServicioHasProducto ordentrabajoServicioHasProducto)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(ordentrabajoServicioHasProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", ordentrabajoServicioHasProducto.IdProducto);
            ViewData["IdServicioOrdenTrabajo"] = new SelectList(_context.ServicioHasOrdentrabajos, "IdServicioOrdenTrabajo", "IdServicioOrdenTrabajo", ordentrabajoServicioHasProducto.IdServicioOrdenTrabajo);
            return View(ordentrabajoServicioHasProducto);
        }

        // GET: OrdentrabajoServicioHasProducto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrdentrabajoServicioHasProductos == null)
            {
                return NotFound();
            }

            var ordentrabajoServicioHasProducto = await _context.OrdentrabajoServicioHasProductos.FindAsync(id);
            if (ordentrabajoServicioHasProducto == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", ordentrabajoServicioHasProducto.IdProducto);
            ViewData["IdServicioOrdenTrabajo"] = new SelectList(_context.ServicioHasOrdentrabajos, "IdServicioOrdenTrabajo", "IdServicioOrdenTrabajo", ordentrabajoServicioHasProducto.IdServicioOrdenTrabajo);
            return View(ordentrabajoServicioHasProducto);
        }

        // POST: OrdentrabajoServicioHasProducto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrdentrabajoServicioHasProductos,IdServicioOrdenTrabajo,IdProducto,PrecioProductoOrdentrabajoServicioHasProductos")] OrdentrabajoServicioHasProducto ordentrabajoServicioHasProducto)
        {
            if (id != ordentrabajoServicioHasProducto.IdOrdentrabajoServicioHasProductos)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordentrabajoServicioHasProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdentrabajoServicioHasProductoExists(ordentrabajoServicioHasProducto.IdOrdentrabajoServicioHasProductos))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", ordentrabajoServicioHasProducto.IdProducto);
            ViewData["IdServicioOrdenTrabajo"] = new SelectList(_context.ServicioHasOrdentrabajos, "IdServicioOrdenTrabajo", "IdServicioOrdenTrabajo", ordentrabajoServicioHasProducto.IdServicioOrdenTrabajo);
            return View(ordentrabajoServicioHasProducto);
        }

        // GET: OrdentrabajoServicioHasProducto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrdentrabajoServicioHasProductos == null)
            {
                return NotFound();
            }

            var ordentrabajoServicioHasProducto = await _context.OrdentrabajoServicioHasProductos
                .Include(o => o.IdProductoNavigation)
                .Include(o => o.IdServicioOrdenTrabajoNavigation)
                .FirstOrDefaultAsync(m => m.IdOrdentrabajoServicioHasProductos == id);
            if (ordentrabajoServicioHasProducto == null)
            {
                return NotFound();
            }

            return View(ordentrabajoServicioHasProducto);
        }

        // POST: OrdentrabajoServicioHasProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrdentrabajoServicioHasProductos == null)
            {
                return Problem("Entity set 'MotorcycleContext.OrdentrabajoServicioHasProductos'  is null.");
            }
            var ordentrabajoServicioHasProducto = await _context.OrdentrabajoServicioHasProductos.FindAsync(id);
            if (ordentrabajoServicioHasProducto != null)
            {
                _context.OrdentrabajoServicioHasProductos.Remove(ordentrabajoServicioHasProducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdentrabajoServicioHasProductoExists(int id)
        {
          return (_context.OrdentrabajoServicioHasProductos?.Any(e => e.IdOrdentrabajoServicioHasProductos == id)).GetValueOrDefault();
        }
    }
}
