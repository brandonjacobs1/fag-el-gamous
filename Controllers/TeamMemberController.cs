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

namespace fag_el_gamous.Controllers
{
    public class TeamMemberController : Controller
    {
        private readonly postgresContext _context;

        public TeamMemberController(postgresContext context)
        {
            _context = context;
        }

        // GET: TeamMember
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Index()
        {
              return _context.Teammembers != null ? 
                          View(await _context.Teammembers.ToListAsync()) :
                          Problem("Entity set 'postgresContext.Teammembers'  is null.");
        }

        // GET: TeamMember/Details/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Teammembers == null)
            {
                return NotFound();
            }

            var teammember = await _context.Teammembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teammember == null)
            {
                return NotFound();
            }

            return View(teammember);
        }

        // GET: TeamMember/Create
        [Authorize(Roles = "Admin, Researcher")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeamMember/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Bio,Membername")] Teammember teammember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teammember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teammember);
        }

        // GET: TeamMember/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Teammembers == null)
            {
                return NotFound();
            }

            var teammember = await _context.Teammembers.FindAsync(id);
            if (teammember == null)
            {
                return NotFound();
            }
            return View(teammember);
        }

        // POST: TeamMember/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Bio,Membername")] Teammember teammember)
        {
            if (id != teammember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teammember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeammemberExists(teammember.Id))
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
            return View(teammember);
        }

        // GET: TeamMember/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Teammembers == null)
            {
                return NotFound();
            }

            var teammember = await _context.Teammembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teammember == null)
            {
                return NotFound();
            }

            return View(teammember);
        }

        // POST: TeamMember/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Teammembers == null)
            {
                return Problem("Entity set 'postgresContext.Teammembers'  is null.");
            }
            var teammember = await _context.Teammembers.FindAsync(id);
            if (teammember != null)
            {
                _context.Teammembers.Remove(teammember);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Researcher")]
        private bool TeammemberExists(long id)
        {
          return (_context.Teammembers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
