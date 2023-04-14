using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fag_el_gamous.Data;
using fag_el_gamous.Models;
using Microsoft.AspNetCore.Authorization;

namespace fag_el_gamous.Views
{
    public class DimensionTextileController : Controller
    {
        private readonly postgresContext _context;

        public DimensionTextileController(postgresContext context)
        {
            _context = context;
        }

        // GET: DimensionTextile
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.DimensionTextiles.Include(d => d.Dimension).Include(d => d.Textile);
            return View(await postgresContext.ToListAsync());
        }

        // GET: DimensionTextile/Details/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.DimensionTextiles == null)
            {
                return NotFound();
            }

            var dimensionTextile = await _context.DimensionTextiles
                .Include(d => d.Dimension)
                .Include(d => d.Textile)
                .FirstOrDefaultAsync(m => m.MainDimensionid == id);
            if (dimensionTextile == null)
            {
                return NotFound();
            }

            return View(dimensionTextile);
        }

        // GET: DimensionTextile/Create
        [Authorize(Roles = "Admin, Researcher")]
        public IActionResult Create()
        {
            ViewData["MainDimensionid"] = new SelectList(_context.Dimensions, "Id", "Id");
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id");
            return View();
        }

        // POST: DimensionTextile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainDimensionid,MainTextileid")] DimensionTextile dimensionTextile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dimensionTextile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainDimensionid"] = new SelectList(_context.Dimensions, "Id", "Id", dimensionTextile.MainDimensionid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", dimensionTextile.MainTextileid);
            return View(dimensionTextile);
        }

        // GET: DimensionTextile/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.DimensionTextiles == null)
            {
                return NotFound();
            }

            var dimensionTextile = await _context.DimensionTextiles.FindAsync(id);
            if (dimensionTextile == null)
            {
                return NotFound();
            }
            ViewData["MainDimensionid"] = new SelectList(_context.Dimensions, "Id", "Id", dimensionTextile.MainDimensionid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", dimensionTextile.MainTextileid);
            return View(dimensionTextile);
        }

        // POST: DimensionTextile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MainDimensionid,MainTextileid")] DimensionTextile dimensionTextile)
        {
            if (id != dimensionTextile.MainDimensionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dimensionTextile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DimensionTextileExists(dimensionTextile.MainDimensionid))
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
            ViewData["MainDimensionid"] = new SelectList(_context.Dimensions, "Id", "Id", dimensionTextile.MainDimensionid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", dimensionTextile.MainTextileid);
            return View(dimensionTextile);
        }

        // GET: DimensionTextile/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.DimensionTextiles == null)
            {
                return NotFound();
            }

            var dimensionTextile = await _context.DimensionTextiles
                .Include(d => d.Dimension)
                .Include(d => d.Textile)
                .FirstOrDefaultAsync(m => m.MainDimensionid == id);
            if (dimensionTextile == null)
            {
                return NotFound();
            }

            return View(dimensionTextile);
        }

        // POST: DimensionTextile/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.DimensionTextiles == null)
            {
                return Problem("Entity set 'postgresContext.DimensionTextiles'  is null.");
            }
            var dimensionTextile = await _context.DimensionTextiles.FindAsync(id);
            if (dimensionTextile != null)
            {
                _context.DimensionTextiles.Remove(dimensionTextile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Researcher")]
        private bool DimensionTextileExists(long id)
        {
          return (_context.DimensionTextiles?.Any(e => e.MainDimensionid == id)).GetValueOrDefault();
        }
    }
}
