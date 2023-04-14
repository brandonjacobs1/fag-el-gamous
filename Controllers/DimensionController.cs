using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fag_el_gamous.Data;
using fag_el_gamous.Models;

namespace fag_el_gamous.Views
{
    public class DimensionController : Controller
    {
        private readonly postgresContext _context;

        public DimensionController(postgresContext context)
        {
            _context = context;
        }

        // GET: Dimension
        public async Task<IActionResult> Index()
        {
              return _context.Dimensions != null ? 
                          View(await _context.Dimensions.ToListAsync()) :
                          Problem("Entity set 'postgresContext.Dimensions'  is null.");
        }

        // GET: Dimension/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Dimensions == null)
            {
                return NotFound();
            }

            var dimension = await _context.Dimensions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dimension == null)
            {
                return NotFound();
            }

            return View(dimension);
        }

        // GET: Dimension/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dimension/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Dimensiontype,Value,Dimensionid")] Dimension dimension)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dimension);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dimension);
        }

        // GET: Dimension/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Dimensions == null)
            {
                return NotFound();
            }

            var dimension = await _context.Dimensions.FindAsync(id);
            if (dimension == null)
            {
                return NotFound();
            }
            return View(dimension);
        }

        // POST: Dimension/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Dimensiontype,Value,Dimensionid")] Dimension dimension)
        {
            if (id != dimension.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dimension);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DimensionExists(dimension.Id))
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
            return View(dimension);
        }

        // GET: Dimension/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Dimensions == null)
            {
                return NotFound();
            }

            var dimension = await _context.Dimensions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dimension == null)
            {
                return NotFound();
            }

            return View(dimension);
        }

        // POST: Dimension/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Dimensions == null)
            {
                return Problem("Entity set 'postgresContext.Dimensions'  is null.");
            }
            var dimension = await _context.Dimensions.FindAsync(id);
            if (dimension != null)
            {
                _context.Dimensions.Remove(dimension);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DimensionExists(long id)
        {
          return (_context.Dimensions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
