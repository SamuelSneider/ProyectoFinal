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
    public class ServicioHasOrdentrabajoController : Controller
    {
        private readonly MotorcycleContext _context;
        private const int PageSize = 10; // Tamaño de página para la paginación

        public ServicioHasOrdentrabajoController(MotorcycleContext context)
        {
            _context = context;
        }

        // GET: ServicioHasOrdentrabajo
        public async Task<IActionResult> Index(string buscar, int page = 1)
        {
            var query = _context.ServicioHasOrdentrabajos
                .Include(s => s.IdOrdenTrabajoNavigation)
                .Include(s => s.IdServicioNavigation)
                .AsQueryable();

            // Búsqueda
            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(s => s.IdOrdenTrabajoNavigation.IdOrdenTrabajo.ToString().Contains(buscar) ||
                                          s.IdServicioNavigation.NombreServicio.Contains(buscar));
            }

            // Paginación
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
            var paginatedItems = await query
                .OrderBy(s => s.IdServicioOrdenTrabajo)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["SearchQuery"] = buscar;

            return View(paginatedItems);
        }

        // GET: ServicioHasOrdentrabajo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ServicioHasOrdentrabajos == null)
            {
                return NotFound();
            }

            var servicioHasOrdentrabajo = await _context.ServicioHasOrdentrabajos
                .Include(s => s.IdOrdenTrabajoNavigation)
                .Include(s => s.IdServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdServicioOrdenTrabajo == id);
            if (servicioHasOrdentrabajo == null)
            {
                return NotFound();
            }

            return View(servicioHasOrdentrabajo);
        }

        // GET: ServicioHasOrdentrabajo/Create
        public IActionResult Create()
        {
            ViewData["IdOrdenTrabajo"] = new SelectList(_context.Ordentrabajos, "IdOrdenTrabajo", "IdOrdenTrabajo");
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "NombreServicio");
            return View();
        }

        // POST: ServicioHasOrdentrabajo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServicioOrdenTrabajo,IdServicio,IdOrdenTrabajo,PrecioServicioHasOrdenTrabajo")] ServicioHasOrdentrabajo servicioHasOrdentrabajo)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(servicioHasOrdentrabajo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOrdenTrabajo"] = new SelectList(_context.Ordentrabajos, "IdOrdenTrabajo", "IdOrdenTrabajo", servicioHasOrdentrabajo.IdOrdenTrabajo);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "NombreServicio", servicioHasOrdentrabajo.IdServicio);
            return View(servicioHasOrdentrabajo);
        }

        // GET: ServicioHasOrdentrabajo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ServicioHasOrdentrabajos == null)
            {
                return NotFound();
            }

            var servicioHasOrdentrabajo = await _context.ServicioHasOrdentrabajos.FindAsync(id);
            if (servicioHasOrdentrabajo == null)
            {
                return NotFound();
            }
            ViewData["IdOrdenTrabajo"] = new SelectList(_context.Ordentrabajos, "IdOrdenTrabajo", "IdOrdenTrabajo", servicioHasOrdentrabajo.IdOrdenTrabajo);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "NombreServicio", servicioHasOrdentrabajo.IdServicio);
            return View(servicioHasOrdentrabajo);
        }

        // POST: ServicioHasOrdentrabajo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServicioOrdenTrabajo,IdServicio,IdOrdenTrabajo,PrecioServicioHasOrdenTrabajo")] ServicioHasOrdentrabajo servicioHasOrdentrabajo)
        {
            if (id != servicioHasOrdentrabajo.IdServicioOrdenTrabajo)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicioHasOrdentrabajo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioHasOrdentrabajoExists(servicioHasOrdentrabajo.IdServicioOrdenTrabajo))
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
            ViewData["IdOrdenTrabajo"] = new SelectList(_context.Ordentrabajos, "IdOrdenTrabajo", "IdOrdenTrabajo", servicioHasOrdentrabajo.IdOrdenTrabajo);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "NombreServicio", servicioHasOrdentrabajo.IdServicio);
            return View(servicioHasOrdentrabajo);
        }

        // GET: ServicioHasOrdentrabajo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ServicioHasOrdentrabajos == null)
            {
                return NotFound();
            }

            var servicioHasOrdentrabajo = await _context.ServicioHasOrdentrabajos
                .Include(s => s.IdOrdenTrabajoNavigation)
                .Include(s => s.IdServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdServicioOrdenTrabajo == id);
            if (servicioHasOrdentrabajo == null)
            {
                return NotFound();
            }

            return View(servicioHasOrdentrabajo);
        }

        // POST: ServicioHasOrdentrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ServicioHasOrdentrabajos == null)
            {
                return Problem("Entity set 'MotorcycleContext.ServicioHasOrdentrabajos'  is null.");
            }
            var servicioHasOrdentrabajo = await _context.ServicioHasOrdentrabajos.FindAsync(id);
            if (servicioHasOrdentrabajo != null)
            {
                _context.ServicioHasOrdentrabajos.Remove(servicioHasOrdentrabajo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioHasOrdentrabajoExists(int id)
        {
          return (_context.ServicioHasOrdentrabajos?.Any(e => e.IdServicioOrdenTrabajo == id)).GetValueOrDefault();
        }
    }
}
