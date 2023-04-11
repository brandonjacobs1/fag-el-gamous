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
            var p = new BurialListViewModel {
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
                ageAtDeath = x.Ageatdeath
                
            }).ToListAsync(),
            userSearch = new userSearch()
        };
        
            return p != null ?
                        View(p) :
                        Problem("Entity set 'postgresContext.Burialmains'  is null.");
        }

        [HttpPost]
        public async Task<IActionResult> Index(userSearch search)
        {
            float depth;
            var shortBurialList = _context.Burialmains;
            var x = new BurialListViewModel
            {
                displayBurial = await shortBurialList
        .Select(x => new displayBurial
        {
            
            Id = x.Id,
            Squarenorthsouth = x.Squarenorthsouth,
            Northsouth = x.Northsouth,
            Squareeastwest = x.Squareeastwest,
            Eastwest = x.Eastwest,
            Area = x.Area,
            ageAtDeath = x.Ageatdeath,
            burialNumber = x.Burialnumber,
            depth = x.Depth,
            hairColor = x.Haircolor,
            fieldBookExcavationYear = x.Fieldbookexcavationyear,
            sex = x.Sex
            //textileFunction = x.,
            //textileStructure = x.,
            //Robust = x.,
            //ParietalBlossing = x.,
            //estimateStature = x.,
        })
        .Where(items =>
            (string.IsNullOrEmpty(search.locationString) || items.Squarenorthsouth == search.locationString || items.Northsouth == search.locationString || items.Squareeastwest == search.locationString || items.Eastwest == search.locationString || items.burialNumber == search.locationString || items.Area == search.locationString) 
            && (search.sex == null || items.sex == search.sex)
            && (search.minDepth == null || items.depth >= search.minDepth)
            && (search.maxDepth == null || items.depth <= search.maxDepth)
            && (search.ageAtDeath == null || items.ageAtDeath == search.ageAtDeath)
            && (search.hairColor == null || items.hairColor == search.hairColor)
            && (search.headDirection == null || items.headDirection == search.headDirection)
            && (search.textileFunction == null || items.textileFunction == search.textileFunction)
            && (search.textileStructure == null || items.textileStructure == search.textileStructure)
            && (search.Robust == null || items.Robust == search.Robust)
            && (search.ParietalBlossing == null || items.ParietalBlossing == search.ParietalBlossing)
            && (search.estimateStature == null || items.estimateStature == search.estimateStature)
        )
        .ToListAsync(),
                userSearch = new userSearch
                {
                    locationString = search.locationString,
                    sex = search.sex,
                    minDepth = search.minDepth,
                    maxDepth = search.maxDepth,
                    ageAtDeath = search.ageAtDeath,
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
        //public displayBurial sortBurialList(displayBurial items)
        //{
        //    return items;
        //}

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

