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
    public class YarnmanipulationTextileController : Controller
    {
        private readonly postgresContext _context;

        public YarnmanipulationTextileController(postgresContext context)
        {
            _context = context;
        }

        // GET: YarnmanipulationTextile
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Index()
        {
              return _context.YarnmanipulationTextiles != null ? 
                          View(await _context.YarnmanipulationTextiles.ToListAsync()) :
                          Problem("Entity set 'postgresContext.YarnmanipulationTextiles'  is null.");
        }

        // GET: YarnmanipulationTextile/Details/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.YarnmanipulationTextiles == null)
            {
                return NotFound();
            }

            var yarnmanipulationTextile = await _context.YarnmanipulationTextiles
                .FirstOrDefaultAsync(m => m.MainYarnmanipulationid == id);
            if (yarnmanipulationTextile == null)
            {
                return NotFound();
            }

            return View(yarnmanipulationTextile);
        }

        // GET: YarnmanipulationTextile/Create
        [Authorize(Roles = "Admin, Researcher")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: YarnmanipulationTextile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainYarnmanipulationid,MainTextileid")] YarnmanipulationTextile yarnmanipulationTextile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yarnmanipulationTextile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yarnmanipulationTextile);
        }

        // GET: YarnmanipulationTextile/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.YarnmanipulationTextiles == null)
            {
                return NotFound();
            }

            var yarnmanipulationTextile = await _context.YarnmanipulationTextiles.FindAsync(id);
            if (yarnmanipulationTextile == null)
            {
                return NotFound();
            }
            return View(yarnmanipulationTextile);
        }

        // POST: YarnmanipulationTextile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MainYarnmanipulationid,MainTextileid")] YarnmanipulationTextile yarnmanipulationTextile)
        {
            if (id != yarnmanipulationTextile.MainYarnmanipulationid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yarnmanipulationTextile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YarnmanipulationTextileExists(yarnmanipulationTextile.MainYarnmanipulationid))
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
            return View(yarnmanipulationTextile);
        }

        // GET: YarnmanipulationTextile/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.YarnmanipulationTextiles == null)
            {
                return NotFound();
            }

            var yarnmanipulationTextile = await _context.YarnmanipulationTextiles
                .FirstOrDefaultAsync(m => m.MainYarnmanipulationid == id);
            if (yarnmanipulationTextile == null)
            {
                return NotFound();
            }

            return View(yarnmanipulationTextile);
        }

        // POST: YarnmanipulationTextile/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.YarnmanipulationTextiles == null)
            {
                return Problem("Entity set 'postgresContext.YarnmanipulationTextiles'  is null.");
            }
            var yarnmanipulationTextile = await _context.YarnmanipulationTextiles.FindAsync(id);
            if (yarnmanipulationTextile != null)
            {
                _context.YarnmanipulationTextiles.Remove(yarnmanipulationTextile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Researcher")]
        private bool YarnmanipulationTextileExists(long id)
        {
          return (_context.YarnmanipulationTextiles?.Any(e => e.MainYarnmanipulationid == id)).GetValueOrDefault();
        }
    }
}
