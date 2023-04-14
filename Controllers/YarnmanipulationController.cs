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
    public class YarnmanipulationController : Controller
    {
        private readonly postgresContext _context;

        public YarnmanipulationController(postgresContext context)
        {
            _context = context;
        }

        // GET: Yarnmanipulation
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Index()
        {
              return _context.Yarnmanipulations != null ? 
                          View(await _context.Yarnmanipulations.ToListAsync()) :
                          Problem("Entity set 'postgresContext.Yarnmanipulations'  is null.");
        }

        // GET: Yarnmanipulation/Details/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Yarnmanipulations == null)
            {
                return NotFound();
            }

            var yarnmanipulation = await _context.Yarnmanipulations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yarnmanipulation == null)
            {
                return NotFound();
            }

            return View(yarnmanipulation);
        }

        // GET: Yarnmanipulation/Create
        [Authorize(Roles = "Admin, Researcher")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yarnmanipulation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Thickness,Angle,Manipulation,Material,Count,Component,Ply,Yarnmanipulationid,Direction")] Yarnmanipulation yarnmanipulation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yarnmanipulation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yarnmanipulation);
        }

        // GET: Yarnmanipulation/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Yarnmanipulations == null)
            {
                return NotFound();
            }

            var yarnmanipulation = await _context.Yarnmanipulations.FindAsync(id);
            if (yarnmanipulation == null)
            {
                return NotFound();
            }
            return View(yarnmanipulation);
        }

        // POST: Yarnmanipulation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Thickness,Angle,Manipulation,Material,Count,Component,Ply,Yarnmanipulationid,Direction")] Yarnmanipulation yarnmanipulation)
        {
            if (id != yarnmanipulation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yarnmanipulation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YarnmanipulationExists(yarnmanipulation.Id))
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
            return View(yarnmanipulation);
        }

        // GET: Yarnmanipulation/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Yarnmanipulations == null)
            {
                return NotFound();
            }

            var yarnmanipulation = await _context.Yarnmanipulations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yarnmanipulation == null)
            {
                return NotFound();
            }

            return View(yarnmanipulation);
        }

        // POST: Yarnmanipulation/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Yarnmanipulations == null)
            {
                return Problem("Entity set 'postgresContext.Yarnmanipulations'  is null.");
            }
            var yarnmanipulation = await _context.Yarnmanipulations.FindAsync(id);
            if (yarnmanipulation != null)
            {
                _context.Yarnmanipulations.Remove(yarnmanipulation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Researcher")]
        private bool YarnmanipulationExists(long id)
        {
          return (_context.Yarnmanipulations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
