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
            //ViewData["CurrentSort"] = sortOrder;
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


            var shortBurialList = _context.Detailscleaneddata;
            int pageSize = 10;
            var displayBurial = shortBurialList
                .Select(x => new displayBurial
                {
                    Id = x.Id,
                    Squarenorthsouth = x.Squarenorthsouth,
                    Northsouth = x.Northsouth,
                    Squareeastwest = x.Squareeastwest,
                    Eastwest = x.Eastwest,
                    Area = x.Area,
                    ageAtDeath = x.Ageatdeath,
                    burialNumber = x.BurialnumberX,
                    depth = float.Parse(x.Depth),
                    hairColor = x.Haircolor,
                    headDirection = x.Headdirection,
                    faceBundles = x.Facebundles,
                    fieldBookExcavationYear = x.Fieldbookexcavationyear,
                    sex = x.Sex,
                    text = x.Text,
                    //textileColor
                    //textileFunction = x.,
                    //textileStructure = x.,
                    //Robust = x.,
                    //ParietalBlossing = x.,
                    estimateStature = x.Estimatestature,
                })
                .Where(items =>
                    (string.IsNullOrEmpty(search.locationString) ||
                         items.Squarenorthsouth.ToString() == search.locationString ||
                         items.Northsouth == search.locationString ||
                         items.Squareeastwest.ToString() == search.locationString ||
                         items.Eastwest == search.locationString ||
                         items.burialNumber == search.locationString ||
                         items.Area == search.locationString) && (search.sex == null || items.sex == search.sex)
                    && (search.minDepth == null || items.depth >= search.minDepth)
                    && (search.maxDepth == null || items.depth <= search.maxDepth)
                    && (search.ageAtDeath == null || items.ageAtDeath == search.ageAtDeath)
                    && (search.hairColor == null || items.hairColor == search.hairColor)
                    && (search.headDirection == null || items.headDirection == search.headDirection)
                    && (search.textileFunction == null || items.textileFunction == search.textileFunction)
                    && (search.textileStructure == null || items.textileStructure == search.textileStructure)
                    && (search.faceBundles == null || items.faceBundles == search.faceBundles)
                    && (search.Robust == null || items.Robust == search.Robust)
                    && (search.ParietalBlossing == null || items.ParietalBlossing == search.ParietalBlossing)
                    && (search.minEstimateStature == null || items.estimateStature >= search.minEstimateStature)
                    && (search.maxEstimateStature == null || items.estimateStature <= search.maxEstimateStature)
                    && (string.IsNullOrEmpty(search.locationString) || items.text.Contains(search.text))
                );

            //switch (sortOrder)
            //{
            //    case "locationString":
            //        displayBurial = displayBurial.OrderBy(s => s.locationString);
            //        break;
            //    case "locationString_desc":
            //        displayBurial = displayBurial.OrderByDescending(s => s.locationString);
            //        break;
            //    case "Depth":
            //        displayBurial = displayBurial.OrderBy(s => s.depth);
            //        break;
            //    case "depth_desc":
            //        displayBurial = displayBurial.OrderByDescending(s => s.depth);
            //        break;
            //    case "AgeAtDeath":
            //        displayBurial = displayBurial.OrderBy(s => s.ageAtDeath);
            //        break;
            //    case "ageatdeath_desc":
            //        displayBurial = displayBurial.OrderByDescending(s => s.ageAtDeath);
            //        break;
            //    case "HairColor":
            //        displayBurial = displayBurial.OrderBy(s => s.hairColor);
            //        break;
            //    case "haircolor_desc":
            //        displayBurial = displayBurial.OrderByDescending(s => s.hairColor);
            //        break;
            //    case "FieldBookExcavationYear":
            //        displayBurial = displayBurial.OrderBy(s => s.fieldBookExcavationYear);
            //        break;
            //    case "fieldbookexcavationyear_desc":
            //        displayBurial = displayBurial.OrderByDescending(s => s.fieldBookExcavationYear);
            //        break;
            //    case "Sex":
            //        displayBurial = displayBurial.OrderBy(s => s.sex);
            //        break;
            //    case "sex_desc":
            //        displayBurial = displayBurial.OrderByDescending(s => s.sex);
            //        break;
            //    case "HeadDirection":
            //        displayBurial = displayBurial.OrderBy(s => s.headDirection);
            //        break;
            //    case "headdirection_desc":
            //        displayBurial = displayBurial.OrderByDescending(s => s.headDirection);
            //        break;
            //    case "TextileStructure":
            //        displayBurial = displayBurial.OrderBy(s => s.textileStructure);
            //        break;
            //    case "textilestructure_desc":
            //        displayBurial = displayBurial.OrderByDescending(s => s.textileStructure);
            //        break;
            //    case "TextileFunction":
            //        displayBurial = displayBurial.OrderBy(s => s.textileFunction);
            //        break;
            //    case "textilefunction_desc":
            //        displayBurial = displayBurial.OrderByDescending(s => s.textileFunction);
            //        break;
            //    case "Robust":
            //        displayBurial = displayBurial.OrderBy(s => s.Robust);
            //        break;
            //    case "robust_desc":
            //        displayBurial = displayBurial.OrderByDescending(s => s.Robust);
            //        break;
            //    case "ParietalBlossing":
            //        displayBurial = displayBurial.OrderBy(s => s.ParietalBlossing);
            //        break;
            //    case "parietalblossing_desc":
            //        displayBurial = displayBurial.OrderByDescending(s => s.ParietalBlossing);
            //        break;
            //    case "EstimateStature":
            //        displayBurial = displayBurial.OrderBy(s => s.estimateStature);
            //        break;
            //    case "estimatestature_desc":
            //        displayBurial = displayBurial.OrderByDescending(s => s.estimateStature);
            //        break;
            //    default:
            //        displayBurial = displayBurial.OrderBy(s => s.locationString);
            //        break;
            //}


            var x = new BurialListViewModel {
                displayBurial = await PaginatedList<displayBurial>.CreateAsync(displayBurial.AsNoTracking(), pageNumber ?? 1, pageSize),
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
                        //Robust = search.Robust,
                        //ParietalBlossing = search.ParietalBlossing,
                        minEstimateStature = search.minEstimateStature,
                        maxEstimateStature = search.maxEstimateStature,
                        text = search.text
                    }
                };
            return x != null ?
                        View(x) :
                        Problem("Entity set 'postgresContext.Burialmains'  is null.");
        
    }

        // Forward the details page to the cleaned data controller
        public async Task<IActionResult> Details(string id)
        {
            return RedirectToAction("Details", "Detailscleaneddatum", new { id = id});
        }
    }
}
