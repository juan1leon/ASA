using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASA.Models;

namespace ASA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatLibroes1Controller : ControllerBase
    {
        private readonly LibrosAsaContext _context;

        public CatLibroes1Controller(LibrosAsaContext context)
        {
            _context = context;
        }

        // GET: api/CatLibroes1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatLibro>>> GetCatLibros()
        {
            return await _context.CatLibros.ToListAsync();
        }

        // GET: api/CatLibroes1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatLibro>> GetCatLibro(int id)
        {
            var catLibro = await _context.CatLibros.FindAsync(id);

            if (catLibro == null)
            {
                return NotFound();
            }

            return catLibro;
        }

        // PUT: api/CatLibroes1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatLibro(int id, CatLibro catLibro)
        {
            if (id != catLibro.IdLibro)
            {
                return BadRequest();
            }

            _context.Entry(catLibro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatLibroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CatLibroes1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CatLibro>> PostCatLibro(CatLibro catLibro)
        {
            _context.CatLibros.Add(catLibro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatLibro", new { id = catLibro.IdLibro }, catLibro);
        }

        // DELETE: api/CatLibroes1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatLibro(int id)
        {
            var catLibro = await _context.CatLibros.FindAsync(id);
            if (catLibro == null)
            {
                return NotFound();
            }

            _context.CatLibros.Remove(catLibro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatLibroExists(int id)
        {
            return _context.CatLibros.Any(e => e.IdLibro == id);
        }
    }
}
