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
    public class DetailscleaneddatumController : Controller
    {
        private readonly postgresContext _context;

        public DetailscleaneddatumController(postgresContext context)
        {
            _context = context;
        }

        // GET: Detailscleaneddatum
        public async Task<IActionResult> Index()
        {
              return _context.Detailscleaneddata != null ? 
                          View(await _context.Detailscleaneddata.ToListAsync()) :
                          Problem("Entity set 'postgresContext.Detailscleaneddata'  is null.");
        }

        // GET: Detailscleaneddatum/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Detailscleaneddata == null)
            {
                return NotFound();
            }

            var detailscleaneddatum = await _context.Detailscleaneddata
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detailscleaneddatum == null)
            {
                return NotFound();
            }

            return View(detailscleaneddatum);
        }

        // GET: Detailscleaneddatum/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Detailscleaneddatum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Squarenorthsouth,Headdirection,Sex,Northsouth,Depth,Eastwest,Adultsubadult,Facebundles,Southtohead,Preservation,Fieldbookpage,Squareeastwest,Goods,Text,Wrapping,Haircolor,Westtohead,Samplescollected,Area,Length,BurialnumberX,Dataexpertinitials,Westtofeet,Ageatdeath,Southtofeet,Fieldbookexcavationyear,Clusternumber,Burialnumber,Dateofexamination,Preservationindex,Observations,Robust,Supraorbitalridges,Orbitedge,Parietalbossing,Gonion,Nuchalcrest,Zygomaticcrest,Sphenooccipitalsynchrondrosis,Lamboidsuture,Squamossuture,Toothattrition,Tootheruption,Tootheruptionageestimate,Ventralarc,Subpubicangle,Sciaticnotch,Pubicbone,PreauricularsulcusBoolean,MedialIpRamus,DorsalpittingBoolean,Femur,Humerus,Femurheaddiameter,Humerusheaddiameter,Femurlength,Humeruslength,Estimatestature,Osteophytosis,CariesPeriodontalDisease,Notes,Unnamed40,Locale,Description,BurialnumberY,Sampledate,Photographeddate,IdY,DonebyX,AnalysistypeX,DateX,Dimensiontype,ValueX,ValueY,ValueX1,ValueY1,Thickness,Angle,Manipulation,Material,Count,Component,Ply,DirectionY")] Detailscleaneddatum detailscleaneddatum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detailscleaneddatum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detailscleaneddatum);
        }

        // GET: Detailscleaneddatum/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Detailscleaneddata == null)
            {
                return NotFound();
            }

            var detailscleaneddatum = await _context.Detailscleaneddata.FindAsync(id);
            if (detailscleaneddatum == null)
            {
                return NotFound();
            }
            return View(detailscleaneddatum);
        }

        // POST: Detailscleaneddatum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Squarenorthsouth,Headdirection,Sex,Northsouth,Depth,Eastwest,Adultsubadult,Facebundles,Southtohead,Preservation,Fieldbookpage,Squareeastwest,Goods,Text,Wrapping,Haircolor,Westtohead,Samplescollected,Area,Length,BurialnumberX,Dataexpertinitials,Westtofeet,Ageatdeath,Southtofeet,Fieldbookexcavationyear,Clusternumber,Burialnumber,Dateofexamination,Preservationindex,Observations,Robust,Supraorbitalridges,Orbitedge,Parietalbossing,Gonion,Nuchalcrest,Zygomaticcrest,Sphenooccipitalsynchrondrosis,Lamboidsuture,Squamossuture,Toothattrition,Tootheruption,Tootheruptionageestimate,Ventralarc,Subpubicangle,Sciaticnotch,Pubicbone,PreauricularsulcusBoolean,MedialIpRamus,DorsalpittingBoolean,Femur,Humerus,Femurheaddiameter,Humerusheaddiameter,Femurlength,Humeruslength,Estimatestature,Osteophytosis,CariesPeriodontalDisease,Notes,Unnamed40,Locale,Description,BurialnumberY,Sampledate,Photographeddate,IdY,DonebyX,AnalysistypeX,DateX,Dimensiontype,ValueX,ValueY,ValueX1,ValueY1,Thickness,Angle,Manipulation,Material,Count,Component,Ply,DirectionY")] Detailscleaneddatum detailscleaneddatum)
        {
            if (id != detailscleaneddatum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detailscleaneddatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailscleaneddatumExists(detailscleaneddatum.Id))
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
            return View(detailscleaneddatum);
        }

        // GET: Detailscleaneddatum/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Detailscleaneddata == null)
            {
                return NotFound();
            }

            var detailscleaneddatum = await _context.Detailscleaneddata
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detailscleaneddatum == null)
            {
                return NotFound();
            }

            return View(detailscleaneddatum);
        }

        // POST: Detailscleaneddatum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Detailscleaneddata == null)
            {
                return Problem("Entity set 'postgresContext.Detailscleaneddata'  is null.");
            }
            var detailscleaneddatum = await _context.Detailscleaneddata.FindAsync(id);
            if (detailscleaneddatum != null)
            {
                _context.Detailscleaneddata.Remove(detailscleaneddatum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailscleaneddatumExists(string id)
        {
          return (_context.Detailscleaneddata?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
