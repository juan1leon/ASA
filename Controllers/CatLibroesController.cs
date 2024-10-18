using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASA.Models;

namespace ASA.Controllers
{
    public class CatLibroesController : Controller
    {
        private readonly LibrosAsaContext _context;

        public CatLibroesController(LibrosAsaContext context)
        {
            _context = context;
        }

        // GET: CatLibroes
        public async Task<IActionResult> Index()
        {
            var librosAsaContext = _context.CatLibros.Include(c => c.IdCategoriaNavigation).Include(c => c.IdEstadoNavigation);
            return View(await librosAsaContext.ToListAsync());
        }

        // GET: CatLibroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catLibro = await _context.CatLibros
                .Include(c => c.IdCategoriaNavigation)
                .Include(c => c.IdEstadoNavigation)
                .FirstOrDefaultAsync(m => m.IdLibro == id);
            if (catLibro == null)
            {
                return NotFound();
            }

            return View(catLibro);
        }

        // GET: CatLibroes/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.CatCategorias, "IdCategoria", "Categoria");
            ViewData["IdEstado"] = new SelectList(_context.CatEstados, "IdEstado", "Estado");
            return View();
        }

        // POST: CatLibroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLibro,Nombre,IdCategoria,IdEstado")] CatLibro catLibro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catLibro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.CatCategorias, "IdCategoria", "Categoria", catLibro.IdCategoria);
            ViewData["IdEstado"] = new SelectList(_context.CatEstados, "IdEstado", "Estado", catLibro.IdEstado);
            return View(catLibro);
        }

        // GET: CatLibroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catLibro = await _context.CatLibros.FindAsync(id);
            if (catLibro == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.CatCategorias, "IdCategoria", "Categoria", catLibro.IdCategoria);
            ViewData["IdEstado"] = new SelectList(_context.CatEstados, "IdEstado", "Estado", catLibro.IdEstado);
            return View(catLibro);
        }

        // POST: CatLibroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLibro,Nombre,IdCategoria,IdEstado")] CatLibro catLibro)
        {
            if (id != catLibro.IdLibro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catLibro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatLibroExists(catLibro.IdLibro))
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
            ViewData["IdCategoria"] = new SelectList(_context.CatCategorias, "IdCategoria", "Categoria", catLibro.IdCategoria);
            ViewData["IdEstado"] = new SelectList(_context.CatEstados, "IdEstado", "Estado", catLibro.IdEstado);
            return View(catLibro);
        }

        // GET: CatLibroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catLibro = await _context.CatLibros
                .Include(c => c.IdCategoriaNavigation)
                .Include(c => c.IdEstadoNavigation)
                .FirstOrDefaultAsync(m => m.IdLibro == id);
            if (catLibro == null)
            {
                return NotFound();
            }

            return View(catLibro);
        }

        // POST: CatLibroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catLibro = await _context.CatLibros.FindAsync(id);
            if (catLibro != null)
            {
                _context.CatLibros.Remove(catLibro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatLibroExists(int id)
        {
            return _context.CatLibros.Any(e => e.IdLibro == id);
        }
    }
}
