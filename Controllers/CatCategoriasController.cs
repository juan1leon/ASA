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
    public class CatCategoriasController : Controller
    {
        private readonly LibrosAsaContext _context;

        public CatCategoriasController(LibrosAsaContext context)
        {
            _context = context;
        }

        // GET: CatCategorias
        public async Task<IActionResult> Index()
        {
            var librosAsaContext = _context.CatCategorias.Include(c => c.IdSubcategoriaNavigation);
            return View(await librosAsaContext.ToListAsync());
        }

        // GET: CatCategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catCategoria = await _context.CatCategorias
                .Include(c => c.IdSubcategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (catCategoria == null)
            {
                return NotFound();
            }

            return View(catCategoria);
        }

        // GET: CatCategorias/Create
        public IActionResult Create()
        {
            ViewData["IdSubcategoria"] = new SelectList(_context.CatSubcategorias, "IdSubcategoria", "Subcategoria");
            return View();
        }

        // POST: CatCategorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategoria,Categoria,IdSubcategoria")] CatCategoria catCategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catCategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSubcategoria"] = new SelectList(_context.CatSubcategorias, "IdSubcategoria", "Subcategoria", catCategoria.IdSubcategoria);
            return View(catCategoria);
        }

        // GET: CatCategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catCategoria = await _context.CatCategorias.FindAsync(id);
            if (catCategoria == null)
            {
                return NotFound();
            }
            ViewData["IdSubcategoria"] = new SelectList(_context.CatSubcategorias, "IdSubcategoria", "Subcategoria", catCategoria.IdSubcategoria);
            return View(catCategoria);
        }

        // POST: CatCategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategoria,Categoria,IdSubcategoria")] CatCategoria catCategoria)
        {
            if (id != catCategoria.IdCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catCategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatCategoriaExists(catCategoria.IdCategoria))
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
            ViewData["IdSubcategoria"] = new SelectList(_context.CatSubcategorias, "IdSubcategoria", "Subcategoria", catCategoria.IdSubcategoria);
            return View(catCategoria);
        }

        // GET: CatCategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catCategoria = await _context.CatCategorias
                .Include(c => c.IdSubcategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (catCategoria == null)
            {
                return NotFound();
            }

            return View(catCategoria);
        }

        // POST: CatCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catCategoria = await _context.CatCategorias.FindAsync(id);
            if (catCategoria != null)
            {
                _context.CatCategorias.Remove(catCategoria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatCategoriaExists(int id)
        {
            return _context.CatCategorias.Any(e => e.IdCategoria == id);
        }
    }
}
