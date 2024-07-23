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
    public class MovilclientesController : Controller
    {
        private readonly MotorcycleContext _context;

        public MovilclientesController(MotorcycleContext context)
        {
            _context = context;
        }

        // GET: Movilclientes
        public async Task<IActionResult> Index()
        {
            var motorcycleContext = _context.Movilclientes.Include(m => m.IdClienteNavigation);
            return View(await motorcycleContext.ToListAsync());
        }

        // GET: Movilclientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movilclientes == null)
            {
                return NotFound();
            }

            var movilcliente = await _context.Movilclientes
                .Include(m => m.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdMovilCliente == id);
            if (movilcliente == null)
            {
                return NotFound();
            }

            return View(movilcliente);
        }

        // GET: Movilclientes/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente");
            return View();
        }

        // POST: Movilclientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMovilCliente,NumeroMovilCliente,IdCliente")] Movilcliente movilcliente)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(movilcliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", movilcliente.IdCliente);
            return View(movilcliente);
        }

        // GET: Movilclientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movilclientes == null)
            {
                return NotFound();
            }

            var movilcliente = await _context.Movilclientes.FindAsync(id);
            if (movilcliente == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", movilcliente.IdCliente);
            return View(movilcliente);
        }

        // POST: Movilclientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMovilCliente,NumeroMovilCliente,IdCliente")] Movilcliente movilcliente)
        {
            if (id != movilcliente.IdMovilCliente)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(movilcliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovilclienteExists(movilcliente.IdMovilCliente))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", movilcliente.IdCliente);
            return View(movilcliente);
        }

        // GET: Movilclientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movilclientes == null)
            {
                return NotFound();
            }

            var movilcliente = await _context.Movilclientes
                .Include(m => m.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdMovilCliente == id);
            if (movilcliente == null)
            {
                return NotFound();
            }

            return View(movilcliente);
        }

        // POST: Movilclientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movilclientes == null)
            {
                return Problem("Entity set 'MotorcycleContext.Movilclientes'  is null.");
            }
            var movilcliente = await _context.Movilclientes.FindAsync(id);
            if (movilcliente != null)
            {
                _context.Movilclientes.Remove(movilcliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovilclienteExists(int id)
        {
          return (_context.Movilclientes?.Any(e => e.IdMovilCliente == id)).GetValueOrDefault();
        }
    }
}
