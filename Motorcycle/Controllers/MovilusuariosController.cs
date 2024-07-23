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
    public class MovilusuariosController : Controller
    {
        private readonly MotorcycleContext _context;

        public MovilusuariosController(MotorcycleContext context)
        {
            _context = context;
        }

        // GET: Movilusuarios
        public async Task<IActionResult> Index()
        {
            var motorcycleContext = _context.Movilusuarios.Include(m => m.IdUsuarioNavigation);
            return View(await motorcycleContext.ToListAsync());
        }

        // GET: Movilusuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movilusuarios == null)
            {
                return NotFound();
            }

            var movilusuario = await _context.Movilusuarios
                .Include(m => m.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdMovilUsuario == id);
            if (movilusuario == null)
            {
                return NotFound();
            }

            return View(movilusuario);
        }

        // GET: Movilusuarios/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View();
        }

        // POST: Movilusuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMovilUsuario,NumeroMovilUsuario,IdUsuario")] Movilusuario movilusuario)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(movilusuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", movilusuario.IdUsuario);
            return View(movilusuario);
        }

        // GET: Movilusuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movilusuarios == null)
            {
                return NotFound();
            }

            var movilusuario = await _context.Movilusuarios.FindAsync(id);
            if (movilusuario == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", movilusuario.IdUsuario);
            return View(movilusuario);
        }

        // POST: Movilusuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMovilUsuario,NumeroMovilUsuario,IdUsuario")] Movilusuario movilusuario)
        {
            if (id != movilusuario.IdMovilUsuario)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(movilusuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovilusuarioExists(movilusuario.IdMovilUsuario))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", movilusuario.IdUsuario);
            return View(movilusuario);
        }

        // GET: Movilusuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movilusuarios == null)
            {
                return NotFound();
            }

            var movilusuario = await _context.Movilusuarios
                .Include(m => m.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdMovilUsuario == id);
            if (movilusuario == null)
            {
                return NotFound();
            }

            return View(movilusuario);
        }

        // POST: Movilusuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movilusuarios == null)
            {
                return Problem("Entity set 'MotorcycleContext.Movilusuarios'  is null.");
            }
            var movilusuario = await _context.Movilusuarios.FindAsync(id);
            if (movilusuario != null)
            {
                _context.Movilusuarios.Remove(movilusuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovilusuarioExists(int id)
        {
          return (_context.Movilusuarios?.Any(e => e.IdMovilUsuario == id)).GetValueOrDefault();
        }
    }
}
