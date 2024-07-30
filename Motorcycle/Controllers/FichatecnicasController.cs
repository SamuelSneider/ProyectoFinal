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
    public class FichatecnicasController : Controller
    {
        private readonly MotorcycleContext _context;
        private const int PageSize = 10; // Tamaño de página


        public FichatecnicasController(MotorcycleContext context)
        {
            _context = context;
        }

        // GET: Fichatecnicas
      
        public async Task<IActionResult> Index(string buscar, int page = 1)
        {
            

            int pageSize = 5; // Tamaño de la página
            var fichatecnicas = _context.Fichatecnicas
                    .Include(f => f.IdClienteNavigation)
                    .Include(f => f.IdUsuarioNavigation)
                    .AsQueryable();
            if (!String.IsNullOrEmpty(buscar))
            {
                fichatecnicas = fichatecnicas.Where(s =>
                                                       s.NumeroMotorFichaTecnica.Contains(buscar) ||
                                                       s.CilindrajeFichaTecnica.Contains(buscar) ||
                                                       s.PlacaFichaTecnica.Contains(buscar) ||
                                                       s.MarcaFichaTecnica.Contains(buscar) ||
                                                       s.ChacisFichaTecnica.Contains(buscar) ||
                                                       s.ModeloFichaTecnica.Contains(buscar) ||
                                                       s.IdClienteNavigation.NombreCliente.Contains(buscar) ||
                                                       s.IdUsuarioNavigation.NombreUsuario.Contains(buscar));
            }

            // Paginación
            int totalItems = await fichatecnicas.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var paginatedFichatecnicas = await fichatecnicas
                .OrderBy(f => f.IdFichaTecnica) // Asegúrate de ordenar para la paginación
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;

            return View(paginatedFichatecnicas);
        }


        // GET: Fichatecnicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fichatecnicas == null)
            {
                return NotFound();
            }

            var fichatecnica = await _context.Fichatecnicas
                .Include(f => f.IdClienteNavigation) 
                .Include(f => f.IdUsuarioNavigation) 
                .FirstOrDefaultAsync(m => m.IdFichaTecnica == id);

            if (fichatecnica == null)
            {
                return NotFound();
            }

            return View(fichatecnica);
        }


        // GET: Fichatecnicas/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View();
        }

        // POST: Fichatecnicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFichaTecnica,FechaRegistroFichaTecnica,NumeroMotorFichaTecnica,CilindrajeFichaTecnica,PlacaFichaTecnica,MarcaFichaTecnica,ChacisFichaTecnica,ModeloFichaTecnica,IdCliente,IdUsuario")] Fichatecnica fichatecnica)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(fichatecnica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", fichatecnica.IdCliente);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", fichatecnica.IdUsuario);
            return View(fichatecnica);
        }

        // GET: Fichatecnicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fichatecnicas == null)
            {
                return NotFound();
            }

            var fichatecnica = await _context.Fichatecnicas.FindAsync(id);
            if (fichatecnica == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", fichatecnica.IdCliente);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", fichatecnica.IdUsuario);
            return View(fichatecnica);
        }

        // POST: Fichatecnicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFichaTecnica,FechaRegistroFichaTecnica,NumeroMotorFichaTecnica,CilindrajeFichaTecnica,PlacaFichaTecnica,MarcaFichaTecnica,ChacisFichaTecnica,ModeloFichaTecnica,IdCliente,IdUsuario")] Fichatecnica fichatecnica)
        {
            if (id != fichatecnica.IdFichaTecnica)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(fichatecnica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FichatecnicaExists(fichatecnica.IdFichaTecnica))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", fichatecnica.IdCliente);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", fichatecnica.IdUsuario);
            return View(fichatecnica);
        }

        // GET: Fichatecnicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fichatecnicas == null)
            {
                return NotFound();
            }

            var fichatecnica = await _context.Fichatecnicas
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdFichaTecnica == id);
            if (fichatecnica == null)
            {
                return NotFound();
            }

            return View(fichatecnica);
        }

        // POST: Fichatecnicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fichatecnicas == null)
            {
                return Problem("Entity set 'MotorcycleContext.Fichatecnicas'  is null.");
            }
            var fichatecnica = await _context.Fichatecnicas.FindAsync(id);
            if (fichatecnica != null)
            {
                _context.Fichatecnicas.Remove(fichatecnica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FichatecnicaExists(int id)
        {
            return (_context.Fichatecnicas?.Any(e => e.IdFichaTecnica == id)).GetValueOrDefault();
        }
    }
}
