using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Motorcycle.Models;
using Motorcycle.ViewModel;

namespace Motorcycle.Controllers
{
    public class ProductoController : Controller
    {
        private readonly MotorcycleContext _context;
        private readonly IWebHostEnvironment _enviroment;

        public ProductoController(MotorcycleContext context, IWebHostEnvironment enviroment)
        {
            _context = context;
            _enviroment = enviroment;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var motorcycleContext = _context.Productos.Include(p => p.IdUsuarioNavigation);
            return View(await motorcycleContext.ToListAsync());
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var producto = new Producto
                {
                    CodigoProducto = viewModel.CodigoProducto,
                    NombreProducto = viewModel.NombreProducto,
                    DescripcionProducto = viewModel.DescripcionProducto,
                    ValorUnitarioProducto = viewModel.ValorUnitarioProducto,
                    IdUsuario = viewModel.IdUsuario
                };

                if (viewModel.FotoFile != null && viewModel.FotoFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await viewModel.FotoFile.CopyToAsync(memoryStream);
                        producto.FotoProducto = memoryStream.ToArray();
                    }
                }

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", viewModel.IdUsuario);
            return View(viewModel);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            var viewModel = new ProductoViewModel
            {
                IdProducto = producto.IdProducto,
                CodigoProducto = producto.CodigoProducto,
                NombreProducto = producto.NombreProducto,
                DescripcionProducto = producto.DescripcionProducto,
                ValorUnitarioProducto = producto.ValorUnitarioProducto,
                IdUsuario = producto.IdUsuario
            };

            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", producto.IdUsuario);
            return View(viewModel);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductoViewModel viewModel)
        {
            if (id != viewModel.IdProducto)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    var producto = await _context.Productos.FindAsync(id);
                    producto.CodigoProducto = viewModel.CodigoProducto;
                    producto.NombreProducto = viewModel.NombreProducto;
                    producto.DescripcionProducto = viewModel.DescripcionProducto;
                    producto.ValorUnitarioProducto = viewModel.ValorUnitarioProducto;
                    producto.IdUsuario = viewModel.IdUsuario;

                    if (viewModel.FotoFile != null && viewModel.FotoFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await viewModel.FotoFile.CopyToAsync(memoryStream);
                            producto.FotoProducto = memoryStream.ToArray();
                        }
                    }

                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(viewModel.IdProducto))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", viewModel.IdUsuario);
            return View(viewModel);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'MotorcycleContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return (_context.Productos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }
    }
}

