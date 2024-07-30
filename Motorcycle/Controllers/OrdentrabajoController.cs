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
    public class OrdentrabajoController : Controller
    {
        private readonly MotorcycleContext _context;
        private const int PageSize = 10; // Tamaño de página para la paginación

        public OrdentrabajoController(MotorcycleContext context)
        {
            _context = context;
        }

        // GET: Ordentrabajo
        public async Task<IActionResult> Index(string buscar, int page = 1)
        {
            var query = _context.Ordentrabajos.Include(o => o.IdCitaNavigation)
                                              .Include(o => o.IdVentaNavigation)
                                              .AsQueryable();

            // Búsqueda
            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(o =>
                     o.NumeroOrdenTrabajo.ToString().Contains(buscar) ||
                     o.IdCitaNavigation.IdCita.ToString().Contains(buscar) ||
                     o.IdVentaNavigation.IdVenta.ToString().Contains(buscar));
            }

            // Paginación
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
            var paginatedItems = await query
                .OrderBy(o => o.IdOrdenTrabajo)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["SearchQuery"] = buscar;

            return View(paginatedItems);
        }


        // GET: Ordentrabajo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ordentrabajos == null)
            {
                return NotFound();
            }

            var ordentrabajo = await _context.Ordentrabajos
                .Include(o => o.IdCitaNavigation)
                .Include(o => o.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.IdOrdenTrabajo == id);
            if (ordentrabajo == null)
            {
                return NotFound();
            }

            return View(ordentrabajo);
        }

        // GET: Ordentrabajo/Create
        public IActionResult Create()
        {
            ViewData["IdCita"] = new SelectList(_context.Cita, "IdCita", "IdCita");
            ViewData["IdVenta"] = new SelectList(_context.Ordenventa, "IdVenta", "IdVenta");
            return View();
        }

        // POST: Ordentrabajo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrdenTrabajo,NumeroOrdenTrabajo,IdCita,IdVenta")] Ordentrabajo ordentrabajo)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(ordentrabajo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCita"] = new SelectList(_context.Cita, "IdCita", "IdCita", ordentrabajo.IdCita);
            ViewData["IdVenta"] = new SelectList(_context.Ordenventa, "IdVenta", "IdVenta", ordentrabajo.IdVenta);
            return View(ordentrabajo);
        }

        // GET: Ordentrabajo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ordentrabajos == null)
            {
                return NotFound();
            }

            var ordentrabajo = await _context.Ordentrabajos.FindAsync(id);
            if (ordentrabajo == null)
            {
                return NotFound();
            }
            ViewData["IdCita"] = new SelectList(_context.Cita, "IdCita", "IdCita", ordentrabajo.IdCita);
            ViewData["IdVenta"] = new SelectList(_context.Ordenventa, "IdVenta", "IdVenta", ordentrabajo.IdVenta);
            return View(ordentrabajo);
        }

        // POST: Ordentrabajo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrdenTrabajo,NumeroOrdenTrabajo,IdCita,IdVenta")] Ordentrabajo ordentrabajo)
        {
            if (id != ordentrabajo.IdOrdenTrabajo)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordentrabajo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdentrabajoExists(ordentrabajo.IdOrdenTrabajo))
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
            ViewData["IdCita"] = new SelectList(_context.Cita, "IdCita", "IdCita", ordentrabajo.IdCita);
            ViewData["IdVenta"] = new SelectList(_context.Ordenventa, "IdVenta", "IdVenta", ordentrabajo.IdVenta);
            return View(ordentrabajo);
        }

        // GET: Ordentrabajo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ordentrabajos == null)
            {
                return NotFound();
            }

            var ordentrabajo = await _context.Ordentrabajos
                .Include(o => o.IdCitaNavigation)
                .Include(o => o.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.IdOrdenTrabajo == id);
            if (ordentrabajo == null)
            {
                return NotFound();
            }

            return View(ordentrabajo);
        }

        // POST: Ordentrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ordentrabajos == null)
            {
                return Problem("Entity set 'MotorcycleContext.Ordentrabajos'  is null.");
            }
            var ordentrabajo = await _context.Ordentrabajos.FindAsync(id);
            if (ordentrabajo != null)
            {
                _context.Ordentrabajos.Remove(ordentrabajo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdentrabajoExists(int id)
        {
          return (_context.Ordentrabajos?.Any(e => e.IdOrdenTrabajo == id)).GetValueOrDefault();
        }
    }
}
