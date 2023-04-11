using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fag_el_gamous.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fag_el_gamous.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fag_el_gamous.Controllers
{
    public class BurialListController : Controller
    {
        private readonly postgresContext _context;

        public BurialListController(postgresContext context)
        {
            _context = context;
        }
        [HttpGet]
        
        public async Task<IActionResult> Index()
        {
            var shortBurialList = _context.Burialmains;
            var x = new BurialListViewModel {
            displayBurial = await shortBurialList.Select(x => new displayBurial
            {
                Id = x.Id,
                Squarenorthsouth = x.Squarenorthsouth,
                Northsouth = x.Northsouth,
                Squareeastwest = x.Squareeastwest,
                Eastwest = x.Eastwest,
                Area = x.Area,
                burialNumber = x.Burialnumber,
                depth = x.Depth,
                hairColor = x.Hair,
                fieldBookExcavationYear = x.Fieldbookexcavationyear,
                sex = x.Sex,
                
            }).ToListAsync(),
            userSearch = new userSearch()
        };
        
            return x != null ?
                        View(x) :
                        Problem("Entity set 'postgresContext.Burialmains'  is null.");
        }

        [HttpPost]
        public async Task<IActionResult> Index(userSearch search)
        {
            var shortBurialList = _context.Burialmains;
            var x = new BurialListViewModel
            {

                displayBurial = await shortBurialList.Select(x => new displayBurial
                {
                    Id = x.Id,
                    Squarenorthsouth = x.Squarenorthsouth,
                    Northsouth = x.Northsouth,
                    Squareeastwest = x.Squareeastwest,
                    Eastwest = x.Eastwest,
                    Area = x.Area,
                    burialNumber = x.Burialnumber,
                    depth = x.Depth,
                    hairColor = x.Hair,
                    fieldBookExcavationYear = x.Fieldbookexcavationyear,
                    sex = x.Sex,
                    //textileFunction = x.,
                    //textileStructure = x.,
                    //Robust = x.,
                    //ParietalBlossing = x.,
                    //estimateStature = x.,
                }).ToListAsync(),
                userSearch = new userSearch
                {
                    locationString = search.locationString,
                    sex = search.sex,
                    minDepth = search.minDepth,
                    maxDepth = search.maxDepth,
                    maxAgeAtDeath = search.maxAgeAtDeath,
                    minAgeAtDeath = search.minAgeAtDeath,
                    hairColor = search.hairColor,
                    headDirection = search.headDirection,
                    textileFunction = search.textileFunction,
                    textileStructure = search.textileStructure,
                    Robust = search.Robust,
                    ParietalBlossing = search.ParietalBlossing,
                    estimateStature = search.estimateStature
                }
            };

            return x != null ?
                        View(x) :
                        Problem("Entity set 'postgresContext.Burialmains'  is null.");
        }


        // GET: Burialmain/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Burialmains == null)
            {
                return NotFound();
            }

            var burialmain = await _context.Burialmains
                .FirstOrDefaultAsync(m => m.Id == id);
            if (burialmain == null)
            {
                return NotFound();
            }

            return View(burialmain);
        }
    }
}

