using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fag_el_gamous.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fag_el_gamous.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Humanizer.On;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;

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
        
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, userSearch? search = null)
        {
            ViewData["CurrentSort"] = sortOrder;
            //ViewData["locationParam"] = sortOrder == "locationString" ? "locationString_desc" : "locationString";
            //ViewData["IdSortParm"] = sortOrder == "Id" ? "id_desc" : "Id";
            //ViewData["depthSortParm"] = sortOrder == "Depth" ? "depth_desc" : "Depth";
            //ViewData["ageAtDeathSortParm"] = sortOrder == "AgeAtDeath" ? "ageatdeath_desc" : "AgeAtDeath";
            //ViewData["hairColorSortParm"] = sortOrder == "HairColor" ? "haircolor_desc" : "HairColor";
            //ViewData["fieldBookExcavationYearSortParm"] = sortOrder == "FieldBookExcavationYear" ? "fieldbookexcavationyear_desc" : "FieldBookExcavationYear";
            //ViewData["sexSortParm"] = sortOrder == "Sex" ? "sex_desc" : "Sex";
            //ViewData["headDirectionSortParm"] = sortOrder == "HeadDirection" ? "headdirection_desc" : "HeadDirection";
            //ViewData["textileStructureSortParm"] = sortOrder == "TextileStructure" ? "textilestructure_desc" : "TextileStructure";
            //ViewData["textileFunctionSortParm"] = sortOrder == "TextileFunction" ? "textilefunction_desc" : "TextileFunction";
            //ViewData["RobustSortParm"] = sortOrder == "Robust" ? "robust_desc" : "Robust";
            //ViewData["ParietalBlossingSortParm"] = sortOrder == "ParietalBlossing" ? "parietalblossing_desc" : "ParietalBlossing";
            //ViewData["estimateStatureSortParm"] = sortOrder == "EstimateStature" ? "estimatestature_desc" : "EstimateStature";

            var burialmainTextile = _context.BurialmainTextiles.Include(x => x.Burialmain).Include(x => x.Textile);

            var viewmodel = burialmainTextile
                .Select(x => new idknameyet
                {
                    Id = x.Burialmain.Id,
                    Squarenorthsouth = x.Burialmain.Squarenorthsouth,
                    Northsouth = x.Burialmain.Northsouth,
                    Squareeastwest = x.Burialmain.Squareeastwest,
                    Eastwest = x.Burialmain.Eastwest,
                    Area = x.Burialmain.Area,
                    AgeAtDeath = x.Burialmain.Ageatdeath,
                    BurialNumber = x.Burialmain.Burialnumber,
                    Depth = x.Burialmain.Depth,
                    HairColor = x.Burialmain.Haircolor,
                    HeadDirection = x.Burialmain.Headdirection,
                    FaceBundles = x.Burialmain.Facebundles,
                    FieldBookExcavationYear = x.Burialmain.Fieldbookexcavationyear,
                    Sex = x.Burialmain.Sex,
                    Text = x.Burialmain.Text,
                    textileColor = _context.ColorTextiles
                        .Include(p => p.Textile)
                        .Include(p => p.Color)
                        .Where(p => p.MainTextileid == x.MainTextileid) //&& (search.TextileColor == null || p.Color.Value.Contains(search.TextileColor)))
                        //.GroupBy(p => p.MainTextileid)
                        //.Select(g => g.First())
                        .ToList(),
                    textileStructure = _context.StructureTextiles
                        .Include(p => p.Textile)
                        .Include(p => p.Structure)
                        .Where(p => p.MainTextileid == x.MainTextileid) //&& (search.TextileStructure == null || search.TextileStructure.Count == 0 || search.TextileStructure.Contains(p.Structure.Value)))
                        //.GroupBy(p => p.MainTextileid)
                        //.Select(g => g.First())
                        .ToList(),
                    textileFunction = _context.TextilefunctionTextiles
                        .Include(p => p.Textile)
                        .Include(p => p.Textilefunction)
                        .Where(p => p.MainTextileid == x.MainTextileid) //&& (search.TextileFunction == null || search.TextileFunction.Count == 0 || search.TextileFunction.Contains(p.Textilefunction.Value)))
                        //.GroupBy(p => p.MainTextileid)
                        //.Select(g => g.First())
                        .ToList()
                })
                .Where(items =>
                    (string.IsNullOrEmpty(search.LocationString) ||
                        items.Squarenorthsouth == search.LocationString ||
                        items.Northsouth == search.LocationString ||
                        items.Squareeastwest == search.LocationString ||
                        items.Eastwest == search.LocationString ||
                        items.BurialNumber == search.LocationString ||
                        items.Area == search.LocationString) &&
                    (!search.MinDepth.HasValue || items.Depth >= search.MinDepth) &&
                    (!search.MaxDepth.HasValue || items.Depth <= search.MaxDepth) &&
                    (string.IsNullOrEmpty(search.AgeAtDeath) || items.AgeAtDeath.Contains(search.AgeAtDeath)) &&
                    (string.IsNullOrEmpty(search.HairColor) || items.HairColor.Contains(search.HairColor)) &&
                    (string.IsNullOrEmpty(search.FieldBookExcavationYear) || items.FieldBookExcavationYear.Contains(search.FieldBookExcavationYear)) &&
                    (string.IsNullOrEmpty(search.Sex) || items.Sex == search.Sex) &&
                    (string.IsNullOrEmpty(search.HeadDirection) || items.HeadDirection.Contains(search.HeadDirection)) &&
                    (string.IsNullOrEmpty(search.FaceBundles) || items.FaceBundles.Contains(search.FaceBundles)) &&
                    (string.IsNullOrEmpty(search.Text) || items.Text.Contains(search.Text)) &&
                    (search.TextileColor == null || items.textileColor.Any(q => q.Color.Value.Contains(search.TextileColor)))
                    );

            //var sexList = await _context.Burialmains.Select(x => x.Sex).Distinct().ToListAsync();
            //var headDirectionList = await _context.Burialmains.Select(x => x.Headdirection).Distinct().ToListAsync();
            var textileColorList = await _context.Colors.Select(x => x.Value).Distinct().ToListAsync();
            var textileStructureList = await _context.Structures.Select(x => x.Value).Distinct().ToListAsync();
            var textileFunctionList = await _context.Textilefunctions.Select(x => x.Value).Distinct().ToListAsync();
            int pageSize = 10;
            var viewModel = new filterViewModel
            {
                displayBurial = await PaginatedList<idknameyet>.CreateAsync(viewmodel.AsNoTracking(), pageNumber ?? 1, pageSize),
                
                Search = new userSearch
                {
                    LocationString = search.LocationString,
                    Sex = search.Sex,
                    MinDepth = search.MinDepth,
                    MaxDepth = search.MaxDepth,
                    AgeAtDeath = search.AgeAtDeath,
                    HairColor = search.HairColor,
                    HeadDirection = search.HeadDirection,
                    TextileFunction = search.TextileFunction,
                    TextileStructure = search.TextileStructure,
                    //MinEstimateStature = search.MinEstimateStature,
                    //MaxEstimateStature = search.MaxEstimateStature,
                    Text = search.Text,


                    //SexList = sexList.Select(s => new SelectListItem { Value = s, Text = s }).ToList(),
                    //HeadList = headList.Select(h => new SelectListItem { Value = h, Text = h }).ToList(),
                    SexList = await _context.Burialmains.Select(x => x.Sex).Distinct().ToListAsync(),
                    HeadDirectionList = await _context.Burialmains.Select(x => x.Headdirection).Distinct().ToListAsync(),
                    TextileColorList = textileColorList.Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() }).ToList(),
                    TextileStructureList = textileStructureList.Select(s => new SelectListItem { Value = s.ToString(), Text = s.ToString() }).ToList(),
                    TextileFunctionList = textileFunctionList.Select(f => new SelectListItem { Value = f.ToString(), Text = f.ToString() }).ToList()
                }
        };

            return viewModel != null ?
                        View(viewModel) :
                        Problem("Entity set 'postgresContext.Burialmains'  is null.");



            //    var textileColor = _context.Colors;
            //    var shortBurialList = _context.Burialmains;
            //    int pageSize = 10;
            //    var displayBurial = shortBurialList
            //        .Select(x => new displayBurial
            //        {
            //            Id = x.Id,
            //            Squarenorthsouth = x.Squarenorthsouth,
            //            Northsouth = x.Northsouth,
            //            Squareeastwest = x.Squareeastwest,
            //            Eastwest = x.Eastwest,
            //            Area = x.Area,
            //            ageAtDeath = x.Ageatdeath,
            //            burialNumber = x.Burialnumber,
            //            depth = x.Depth != null ? float.Parse(x.Depth) : 0.0f,
            //            hairColor = x.Haircolor,
            //            headDirection = x.Headdirection,
            //            faceBundles = x.Facebundles,
            //            fieldBookExcavationYear = x.Fieldbookexcavationyear,
            //            sex = x.Sex,
            //            text = x.Text,
            //            //textileColor = ,
            //            //textileFunction = x.,
            //            //textileStructure = x.,
            //            //estimateStature = x.Estimatestature,
            //        })
            //        .Where(items =>
            //            (string.IsNullOrEmpty(search.locationString) ||
            //                 items.Squarenorthsouth == search.locationString ||
            //                 items.Northsouth == search.locationString ||
            //                 items.Squareeastwest == search.locationString ||
            //                 items.Eastwest == search.locationString ||
            //                 items.burialNumber == search.locationString ||
            //                 items.Area == search.locationString)
            //            && (search.sex == null || items.sex == search.sex)
            //            && (search.minDepth == null || items.depth >= search.minDepth)
            //            && (search.maxDepth == null || items.depth <= search.maxDepth)
            //            && (search.ageAtDeath == null || items.ageAtDeath == search.ageAtDeath)
            //            && (search.hairColor == null || items.hairColor == search.hairColor)
            //            && (search.headDirection == null || items.headDirection == search.headDirection)
            //            //&& (search.textileFunction == null || items.textileFunction == search.textileFunction)
            //            //&& (search.textileStructure == null || items.textileStructure == search.textileStructure)
            //            //&& (search.faceBundles == null || items.faceBundles == search.faceBundles)
            //            //&& (search.minEstimateStature == null || items.estimateStature >= search.minEstimateStature)
            //            //&& (search.maxEstimateStature == null || items.estimateStature <= search.maxEstimateStature)
            //            //&& (string.IsNullOrEmpty(search.text) || items.text.Contains(search.text))
            //        );

            //var x = new BurialListViewModel {
            //        displayBurial = await PaginatedList<displayBurial>.CreateAsync(displayBurial.AsNoTracking(), pageNumber ?? 1, pageSize),
            //            userSearch = new userSearch
            //            {
            //                locationString = search.locationString,
            //                sex = search.sex,
            //                minDepth = search.minDepth,
            //                maxDepth = search.maxDepth,
            //                ageAtDeath = search.ageAtDeath,
            //                hairColor = search.hairColor,
            //                headDirection = search.headDirection,
            //                textileFunction = search.textileFunction,
            //                textileStructure = search.textileStructure,
            //                minEstimateStature = search.minEstimateStature,
            //                maxEstimateStature = search.maxEstimateStature,
            //                text = search.text
            //            }
            //        };


        }

        // Forward the details page to the cleaned data controller
        public async Task<IActionResult> Details(string id)
        {
            return RedirectToAction("Details", "DetailsPage", new { id = id});
        }
    }
}
