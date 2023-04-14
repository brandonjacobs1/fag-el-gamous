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
    public class TextilefunctionController : Controller
    {
        private readonly postgresContext _context;

        public TextilefunctionController(postgresContext context)
        {
            _context = context;
        }

        // GET: Textilefunction
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Index()
        {
              return _context.Textilefunctions != null ? 
                          View(await _context.Textilefunctions.ToListAsync()) :
                          Problem("Entity set 'postgresContext.Textilefunctions'  is null.");
        }

        // GET: Textilefunction/Details/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Textilefunctions == null)
            {
                return NotFound();
            }

            var textilefunction = await _context.Textilefunctions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (textilefunction == null)
            {
                return NotFound();
            }

            return View(textilefunction);
        }

        // GET: Textilefunction/Create
        [Authorize(Roles = "Admin, Researcher")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Textilefunction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,Textilefunctionid")] Textilefunction textilefunction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(textilefunction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(textilefunction);
        }

        // GET: Textilefunction/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Textilefunctions == null)
            {
                return NotFound();
            }

            var textilefunction = await _context.Textilefunctions.FindAsync(id);
            if (textilefunction == null)
            {
                return NotFound();
            }
            return View(textilefunction);
        }

        // POST: Textilefunction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Value,Textilefunctionid")] Textilefunction textilefunction)
        {
            if (id != textilefunction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(textilefunction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TextilefunctionExists(textilefunction.Id))
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
            return View(textilefunction);
        }

        // GET: Textilefunction/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Textilefunctions == null)
            {
                return NotFound();
            }

            var textilefunction = await _context.Textilefunctions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (textilefunction == null)
            {
                return NotFound();
            }

            return View(textilefunction);
        }

        // POST: Textilefunction/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Textilefunctions == null)
            {
                return Problem("Entity set 'postgresContext.Textilefunctions'  is null.");
            }
            var textilefunction = await _context.Textilefunctions.FindAsync(id);
            if (textilefunction != null)
            {
                _context.Textilefunctions.Remove(textilefunction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Researcher")]
        private bool TextilefunctionExists(long id)
        {
          return (_context.Textilefunctions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
