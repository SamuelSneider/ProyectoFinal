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
    public class CorreousuariosController : Controller
    {
        private readonly MotorcycleContext _context;
        private const int PageSize = 10; // Tamaño de página

        public CorreousuariosController(MotorcycleContext context)
        {
            _context = context;
        }

        // GET: Correousuarios
        public async Task<IActionResult> Index(string buscar, int page = 1)
        {
            int pageSize = 5; // Tamaño de la página
            var correousuarios = _context.Correousuarios
                .Include(c => c.IdUsuarioNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                correousuarios = correousuarios.Where(s =>
                    s.CorreoUsuario.Contains(buscar) ||
                    s.IdUsuarioNavigation.NombreUsuario.Contains(buscar));
            }

            // Paginación
            int totalItems = await correousuarios.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var paginatedCorreousuarios = await correousuarios
                .OrderBy(c => c.IdCorreoUsuario) // Asegúrate de ordenar para la paginación
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;

            return View(paginatedCorreousuarios);
        }

        // GET: Correousuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Correousuarios == null)
            {
                return NotFound();
            }

            var correousuario = await _context.Correousuarios
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCorreoUsuario == id);
            if (correousuario == null)
            {
                return NotFound();
            }

            return View(correousuario);
        }

        // GET: Correousuarios/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View();
        }

        // POST: Correousuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCorreoUsuario,CorreoUsuario1,IdUsuario")] Correousuario correousuario)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(correousuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", correousuario.IdUsuario);
            return View(correousuario);
        }

        // GET: Correousuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Correousuarios == null)
            {
                return NotFound();
            }

            var correousuario = await _context.Correousuarios.FindAsync(id);
            if (correousuario == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", correousuario.IdUsuario);
            return View(correousuario);
        }

        // POST: Correousuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCorreoUsuario,CorreoUsuario1,IdUsuario")] Correousuario correousuario)
        {
            if (id != correousuario.IdCorreoUsuario)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(correousuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorreousuarioExists(correousuario.IdCorreoUsuario))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", correousuario.IdUsuario);
            return View(correousuario);
        }

        // GET: Correousuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Correousuarios == null)
            {
                return NotFound();
            }

            var correousuario = await _context.Correousuarios
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCorreoUsuario == id);
            if (correousuario == null)
            {
                return NotFound();
            }

            return View(correousuario);
        }

        // POST: Correousuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Correousuarios == null)
            {
                return Problem("Entity set 'MotorcycleContext.Correousuarios'  is null.");
            }
            var correousuario = await _context.Correousuarios.FindAsync(id);
            if (correousuario != null)
            {
                _context.Correousuarios.Remove(correousuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorreousuarioExists(int id)
        {
          return (_context.Correousuarios?.Any(e => e.IdCorreoUsuario == id)).GetValueOrDefault();
        }
    }
}
