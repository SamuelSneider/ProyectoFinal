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
    public class DetalleventasController : Controller
    {
        private readonly MotorcycleContext _context;

        public DetalleventasController(MotorcycleContext context)
        {
            _context = context;
        }

        // GET: Detalleventas
        public async Task<IActionResult> Index()
        {
            var motorcycleContext = _context.Detalleventas.Include(d => d.IdProductoNavigation).Include(d => d.IdVentaNavigation);
            return View(await motorcycleContext.ToListAsync());
        }

        // GET: Detalleventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Detalleventas == null)
            {
                return NotFound();
            }

            var detalleventa = await _context.Detalleventas
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleVentas == id);
            if (detalleventa == null)
            {
                return NotFound();
            }

            return View(detalleventa);
        }

        // GET: Detalleventas/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            ViewData["IdVenta"] = new SelectList(_context.Ordenventa, "IdVenta", "IdVenta");
            return View();
        }

        // POST: Detalleventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleVentas,CantidaDetalleVentas,ValorTotalDetalleVentas,ValorUnitarioDetalleVentas,IdVenta,IdProducto")] Detalleventa detalleventa)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(detalleventa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleventa.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Ordenventa, "IdVenta", "IdVenta", detalleventa.IdVenta);
            return View(detalleventa);
        }

        // GET: Detalleventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Detalleventas == null)
            {
                return NotFound();
            }

            var detalleventa = await _context.Detalleventas.FindAsync(id);
            if (detalleventa == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleventa.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Ordenventa, "IdVenta", "IdVenta", detalleventa.IdVenta);
            return View(detalleventa);
        }

        // POST: Detalleventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalleVentas,CantidaDetalleVentas,ValorTotalDetalleVentas,ValorUnitarioDetalleVentas,IdVenta,IdProducto")] Detalleventa detalleventa)
        {
            if (id != detalleventa.IdDetalleVentas)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleventa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleventaExists(detalleventa.IdDetalleVentas))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleventa.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Ordenventa, "IdVenta", "IdVenta", detalleventa.IdVenta);
            return View(detalleventa);
        }

        // GET: Detalleventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Detalleventas == null)
            {
                return NotFound();
            }

            var detalleventa = await _context.Detalleventas
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleVentas == id);
            if (detalleventa == null)
            {
                return NotFound();
            }

            return View(detalleventa);
        }

        // POST: Detalleventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Detalleventas == null)
            {
                return Problem("Entity set 'MotorcycleContext.Detalleventas'  is null.");
            }
            var detalleventa = await _context.Detalleventas.FindAsync(id);
            if (detalleventa != null)
            {
                _context.Detalleventas.Remove(detalleventa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleventaExists(int id)
        {
          return (_context.Detalleventas?.Any(e => e.IdDetalleVentas == id)).GetValueOrDefault();
        }
    }
}
