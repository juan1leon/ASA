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
    public class CatSubcategoriasController : Controller
    {
        private readonly LibrosAsaContext _context;

        public CatSubcategoriasController(LibrosAsaContext context)
        {
            _context = context;
        }

        // GET: CatSubcategorias
        public async Task<IActionResult> Index()
        {
            return View(await _context.CatSubcategorias.ToListAsync());
        }

        // GET: CatSubcategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catSubcategoria = await _context.CatSubcategorias
                .FirstOrDefaultAsync(m => m.IdSubcategoria == id);
            if (catSubcategoria == null)
            {
                return NotFound();
            }

            return View(catSubcategoria);
        }

        // GET: CatSubcategorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CatSubcategorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSubcategoria,Subcategoria")] CatSubcategoria catSubcategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catSubcategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catSubcategoria);
        }

        // GET: CatSubcategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catSubcategoria = await _context.CatSubcategorias.FindAsync(id);
            if (catSubcategoria == null)
            {
                return NotFound();
            }
            return View(catSubcategoria);
        }

        // POST: CatSubcategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSubcategoria,Subcategoria")] CatSubcategoria catSubcategoria)
        {
            if (id != catSubcategoria.IdSubcategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catSubcategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatSubcategoriaExists(catSubcategoria.IdSubcategoria))
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
            return View(catSubcategoria);
        }

        // GET: CatSubcategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catSubcategoria = await _context.CatSubcategorias
                .FirstOrDefaultAsync(m => m.IdSubcategoria == id);
            if (catSubcategoria == null)
            {
                return NotFound();
            }

            return View(catSubcategoria);
        }

        // POST: CatSubcategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catSubcategoria = await _context.CatSubcategorias.FindAsync(id);
            if (catSubcategoria != null)
            {
                _context.CatSubcategorias.Remove(catSubcategoria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatSubcategoriaExists(int id)
        {
            return _context.CatSubcategorias.Any(e => e.IdSubcategoria == id);
        }
    }
}
