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
    public class CitumsController : Controller
    {
        private readonly MotorcycleContext _context;
        private const int PageSize = 10; // Tamaño de página

        public CitumsController(MotorcycleContext context)
        {
            _context = context;
        }

        // GET: Citums
        public async Task<IActionResult> Index(string buscar, int page = 1)
        {
            int pageSize = 5;
            var citas = _context.Cita
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .Include(c => c.IdFichaTecnicaNavigation) // Asegúrate de incluir esta propiedad si la usas en la vista
                .AsQueryable();

            if (!String.IsNullOrEmpty(buscar))
            {
                citas = citas.Where(c =>
                    c.IdClienteNavigation.NombreCliente.Contains(buscar) ||
                    c.IdUsuarioNavigation.NombreUsuario.Contains(buscar));
            }

            // Paginación
            int totalItems = await citas.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var paginatedCitas = await citas
                .OrderBy(c => c.IdFichaTecnica) // Ordena por un campo adecuado para la paginación
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;

            return View(paginatedCitas);
        }


        // GET: Citums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var citum = await _context.Cita
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdFichaTecnicaNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (citum == null)
            {
                return NotFound();
            }

            return View(citum);
        }

        // GET: Citums/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente");
            ViewData["IdFichaTecnica"] = new SelectList(_context.Fichatecnicas, "IdFichaTecnica", "IdFichaTecnica");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View();
        }

        // POST: Citums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,FechaCita,HoraCita,FechaFinalizacionCita,IdFichaTecnica,IdCliente,IdUsuario")] Citum citum)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(citum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", citum.IdCliente);
            ViewData["IdFichaTecnica"] = new SelectList(_context.Fichatecnicas, "IdFichaTecnica", "IdFichaTecnica", citum.IdFichaTecnica);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", citum.IdUsuario);
            return View(citum);
        }

        // GET: Citums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var citum = await _context.Cita.FindAsync(id);
            if (citum == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", citum.IdCliente);
            ViewData["IdFichaTecnica"] = new SelectList(_context.Fichatecnicas, "IdFichaTecnica", "IdFichaTecnica", citum.IdFichaTecnica);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", citum.IdUsuario);
            return View(citum);
        }

        // POST: Citums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,FechaCita,HoraCita,FechaFinalizacionCita,IdFichaTecnica,IdCliente,IdUsuario")] Citum citum)
        {
            if (id != citum.IdCita)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(citum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitumExists(citum.IdCita))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", citum.IdCliente);
            ViewData["IdFichaTecnica"] = new SelectList(_context.Fichatecnicas, "IdFichaTecnica", "IdFichaTecnica", citum.IdFichaTecnica);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", citum.IdUsuario);
            return View(citum);
        }

        // GET: Citums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var citum = await _context.Cita
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdFichaTecnicaNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (citum == null)
            {
                return NotFound();
            }

            return View(citum);
        }

        // POST: Citums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cita == null)
            {
                return Problem("Entity set 'MotorcycleContext.Cita'  is null.");
            }
            var citum = await _context.Cita.FindAsync(id);
            if (citum != null)
            {
                _context.Cita.Remove(citum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitumExists(int id)
        {
          return (_context.Cita?.Any(e => e.IdCita == id)).GetValueOrDefault();
        }
    }
}
