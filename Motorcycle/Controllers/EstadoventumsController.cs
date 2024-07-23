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
    public class EstadoventumsController : Controller
    {
        private readonly MotorcycleContext _context;

        public EstadoventumsController(MotorcycleContext context)
        {
            _context = context;
        }

        // GET: Estadoventums
        public async Task<IActionResult> Index()
        {
              return _context.Estadoventa != null ? 
                          View(await _context.Estadoventa.ToListAsync()) :
                          Problem("Entity set 'MotorcycleContext.Estadoventa'  is null.");
        }

        // GET: Estadoventums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estadoventa == null)
            {
                return NotFound();
            }

            var estadoventum = await _context.Estadoventa
                .FirstOrDefaultAsync(m => m.IdEstadoVenta == id);
            if (estadoventum == null)
            {
                return NotFound();
            }

            return View(estadoventum);
        }

        // GET: Estadoventums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estadoventums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstadoVenta,NombreEstadoVenta")] Estadoventum estadoventum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoventum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoventum);
        }

        // GET: Estadoventums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estadoventa == null)
            {
                return NotFound();
            }

            var estadoventum = await _context.Estadoventa.FindAsync(id);
            if (estadoventum == null)
            {
                return NotFound();
            }
            return View(estadoventum);
        }

        // POST: Estadoventums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstadoVenta,NombreEstadoVenta")] Estadoventum estadoventum)
        {
            if (id != estadoventum.IdEstadoVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoventum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoventumExists(estadoventum.IdEstadoVenta))
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
            return View(estadoventum);
        }

        // GET: Estadoventums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estadoventa == null)
            {
                return NotFound();
            }

            var estadoventum = await _context.Estadoventa
                .FirstOrDefaultAsync(m => m.IdEstadoVenta == id);
            if (estadoventum == null)
            {
                return NotFound();
            }

            return View(estadoventum);
        }

        // POST: Estadoventums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estadoventa == null)
            {
                return Problem("Entity set 'MotorcycleContext.Estadoventa'  is null.");
            }
            var estadoventum = await _context.Estadoventa.FindAsync(id);
            if (estadoventum != null)
            {
                _context.Estadoventa.Remove(estadoventum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoventumExists(int id)
        {
          return (_context.Estadoventa?.Any(e => e.IdEstadoVenta == id)).GetValueOrDefault();
        }
    }
}
