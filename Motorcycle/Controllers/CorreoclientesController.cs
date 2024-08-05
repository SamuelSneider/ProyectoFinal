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
    public class CorreoclientesController : Controller
    {
        private readonly MotorcycleContext _context;
        private const int PageSize = 10; // Tamaño de página

        public CorreoclientesController(MotorcycleContext context)
        {
            _context = context;
        }

        // GET: Correoclientes
        public async Task<IActionResult> Index(string buscar, int page = 1)
        {
            int pageSize = PageSize; // Tamaño de página
            var correoclientes = _context.Correoclientes
                .Include(c => c.IdClienteNavigation)
                .AsQueryable();

            if (!String.IsNullOrEmpty(buscar))
            {
                correoclientes = correoclientes.Where(c =>
                    c.CorreoCliente1.Contains(buscar) || // Usa el nombre de propiedad correcto aquí
                    c.IdClienteNavigation.NombreCliente.Contains(buscar));
            }

            // Paginación
            int totalItems = await correoclientes.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var paginatedCorreoclientes = await correoclientes
                .OrderBy(c => c.IdCorreoCliente) // Ordena por un campo adecuado para la paginación
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["SearchQuery"] = buscar;

            return View(paginatedCorreoclientes);
        }


        // GET: Correoclientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Correoclientes == null)
            {
                return NotFound();
            }

            var correocliente = await _context.Correoclientes
                .Include(c => c.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdCorreoCliente == id);
            if (correocliente == null)
            {
                return NotFound();
            }

            return View(correocliente);
        }

        // GET: Correoclientes/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente");
            return View();
        }

        // POST: Correoclientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCorreoCliente,CorreoCliente1,IdCliente")] Correocliente correocliente)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(correocliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", correocliente.IdCliente);
            return View(correocliente);
        }

        // GET: Correoclientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Correoclientes == null)
            {
                return NotFound();
            }

            var correocliente = await _context.Correoclientes.FindAsync(id);
            if (correocliente == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", correocliente.IdCliente);
            return View(correocliente);
        }

        // POST: Correoclientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCorreoCliente,CorreoCliente1,IdCliente")] Correocliente correocliente)
        {
            if (id != correocliente.IdCorreoCliente)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(correocliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorreoclienteExists(correocliente.IdCorreoCliente))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", correocliente.IdCliente);
            return View(correocliente);
        }

        // GET: Correoclientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Correoclientes == null)
            {
                return NotFound();
            }

            var correocliente = await _context.Correoclientes
                .Include(c => c.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdCorreoCliente == id);
            if (correocliente == null)
            {
                return NotFound();
            }

            return View(correocliente);
        }

        // POST: Correoclientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Correoclientes == null)
            {
                return Problem("Entity set 'MotorcycleContext.Correoclientes'  is null.");
            }
            var correocliente = await _context.Correoclientes.FindAsync(id);
            if (correocliente != null)
            {
                _context.Correoclientes.Remove(correocliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorreoclienteExists(int id)
        {
          return (_context.Correoclientes?.Any(e => e.IdCorreoCliente == id)).GetValueOrDefault();
        }
    }
}
