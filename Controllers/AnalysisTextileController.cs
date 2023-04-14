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
    public class AnalysisTextileController : Controller
    {
        private readonly postgresContext _context;

        public AnalysisTextileController(postgresContext context)
        {
            _context = context;
        }

        // GET: AnalysisTextile
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.AnalysisTextiles.Include(a => a.Analysis).Include(a => a.Textile);
            return View(await postgresContext.ToListAsync());
        }

        // GET: AnalysisTextile/Details/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.AnalysisTextiles == null)
            {
                return NotFound();
            }

            var analysisTextile = await _context.AnalysisTextiles
                .Include(a => a.Analysis)
                .Include(a => a.Textile)
                .FirstOrDefaultAsync(m => m.MainAnalysisid == id);
            if (analysisTextile == null)
            {
                return NotFound();
            }

            return View(analysisTextile);
        }

        // GET: AnalysisTextile/Create
        [Authorize(Roles = "Admin, Researcher")]
        public IActionResult Create()
        {
            ViewData["MainAnalysisid"] = new SelectList(_context.Analyses, "Id", "Id");
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id");
            return View();
        }

        // POST: AnalysisTextile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainAnalysisid,MainTextileid")] AnalysisTextile analysisTextile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(analysisTextile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainAnalysisid"] = new SelectList(_context.Analyses, "Id", "Id", analysisTextile.MainAnalysisid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", analysisTextile.MainTextileid);
            return View(analysisTextile);
        }

        // GET: AnalysisTextile/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.AnalysisTextiles == null)
            {
                return NotFound();
            }

            var analysisTextile = await _context.AnalysisTextiles.FindAsync(id);
            if (analysisTextile == null)
            {
                return NotFound();
            }
            ViewData["MainAnalysisid"] = new SelectList(_context.Analyses, "Id", "Id", analysisTextile.MainAnalysisid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", analysisTextile.MainTextileid);
            return View(analysisTextile);
        }

        // POST: AnalysisTextile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MainAnalysisid,MainTextileid")] AnalysisTextile analysisTextile)
        {
            if (id != analysisTextile.MainAnalysisid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(analysisTextile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalysisTextileExists(analysisTextile.MainAnalysisid))
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
            ViewData["MainAnalysisid"] = new SelectList(_context.Analyses, "Id", "Id", analysisTextile.MainAnalysisid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", analysisTextile.MainTextileid);
            return View(analysisTextile);
        }

        // GET: AnalysisTextile/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.AnalysisTextiles == null)
            {
                return NotFound();
            }

            var analysisTextile = await _context.AnalysisTextiles
                .Include(a => a.Analysis)
                .Include(a => a.Textile)
                .FirstOrDefaultAsync(m => m.MainAnalysisid == id);
            if (analysisTextile == null)
            {
                return NotFound();
            }

            return View(analysisTextile);
        }

        // POST: AnalysisTextile/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.AnalysisTextiles == null)
            {
                return Problem("Entity set 'postgresContext.AnalysisTextiles'  is null.");
            }
            var analysisTextile = await _context.AnalysisTextiles.FindAsync(id);
            if (analysisTextile != null)
            {
                _context.AnalysisTextiles.Remove(analysisTextile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnalysisTextileExists(long id)
        {
          return (_context.AnalysisTextiles?.Any(e => e.MainAnalysisid == id)).GetValueOrDefault();
        }
    }
}
