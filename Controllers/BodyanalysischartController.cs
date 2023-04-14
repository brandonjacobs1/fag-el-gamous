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
    public class BodyanalysischartController : Controller
    {
        private readonly postgresContext _context;

        public BodyanalysischartController(postgresContext context)
        {
            _context = context;
        }

        // GET: Bodyanalysischart
        public async Task<IActionResult> Index()
        {
              return _context.Bodyanalysischarts != null ? 
                          View(await _context.Bodyanalysischarts.ToListAsync()) :
                          Problem("Entity set 'postgresContext.Bodyanalysischarts'  is null.");
        }

        // GET: Bodyanalysischart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bodyanalysischarts == null)
            {
                return NotFound();
            }

            var bodyanalysischart = await _context.Bodyanalysischarts
                .FirstOrDefaultAsync(m => m.Key == id);
            if (bodyanalysischart == null)
            {
                return NotFound();
            }

            return View(bodyanalysischart);
        }

        // GET: Bodyanalysischart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bodyanalysischart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,Squarenorthsouth,Northsouth,Squareeastwest,Eastwest,Area,Burialnumber,Dateofexamination,Preservationindex,Haircolor,Observations,Robust,Supraorbitalridges,Orbitedge,Parietalbossing,Gonion,Nuchalcrest,Zygomaticcrest,Sphenooccipitalsynchrondrosis,Lamboidsuture,Squamossuture,Toothattrition,Tootheruption,Tootheruptionageestimate,Ventralarc,Subpubicangle,Sciaticnotch,Pubicbone,PreauricularsulcusBoolean,MedialIpRamus,DorsalpittingBoolean,Femur,Humerus,Femurheaddiameter,Humerusheaddiameter,Femurlength,Humeruslength,Estimatestature,Osteophytosis,CariesPeriodontalDisease,Notes")] Bodyanalysischart bodyanalysischart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bodyanalysischart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bodyanalysischart);
        }

        // GET: Bodyanalysischart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bodyanalysischarts == null)
            {
                return NotFound();
            }

            var bodyanalysischart = await _context.Bodyanalysischarts.FindAsync(id);
            if (bodyanalysischart == null)
            {
                return NotFound();
            }
            return View(bodyanalysischart);
        }

        // POST: Bodyanalysischart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Key,Squarenorthsouth,Northsouth,Squareeastwest,Eastwest,Area,Burialnumber,Dateofexamination,Preservationindex,Haircolor,Observations,Robust,Supraorbitalridges,Orbitedge,Parietalbossing,Gonion,Nuchalcrest,Zygomaticcrest,Sphenooccipitalsynchrondrosis,Lamboidsuture,Squamossuture,Toothattrition,Tootheruption,Tootheruptionageestimate,Ventralarc,Subpubicangle,Sciaticnotch,Pubicbone,PreauricularsulcusBoolean,MedialIpRamus,DorsalpittingBoolean,Femur,Humerus,Femurheaddiameter,Humerusheaddiameter,Femurlength,Humeruslength,Estimatestature,Osteophytosis,CariesPeriodontalDisease,Notes")] Bodyanalysischart bodyanalysischart)
        {
            if (id != bodyanalysischart.Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bodyanalysischart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BodyanalysischartExists(bodyanalysischart.Key))
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
            return View(bodyanalysischart);
        }

        // GET: Bodyanalysischart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bodyanalysischarts == null)
            {
                return NotFound();
            }

            var bodyanalysischart = await _context.Bodyanalysischarts
                .FirstOrDefaultAsync(m => m.Key == id);
            if (bodyanalysischart == null)
            {
                return NotFound();
            }

            return View(bodyanalysischart);
        }

        // POST: Bodyanalysischart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bodyanalysischarts == null)
            {
                return Problem("Entity set 'postgresContext.Bodyanalysischarts'  is null.");
            }
            var bodyanalysischart = await _context.Bodyanalysischarts.FindAsync(id);
            if (bodyanalysischart != null)
            {
                _context.Bodyanalysischarts.Remove(bodyanalysischart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BodyanalysischartExists(int id)
        {
          return (_context.Bodyanalysischarts?.Any(e => e.Key == id)).GetValueOrDefault();
        }
    }
}
