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

// This is a C# program file. It contains the definition of a controller class that handles HTTP requests related to a list of burials. 
 


namespace fag_el_gamous.Controllers
{
    public class BurialListController : Controller
    {
    private readonly postgresContext _context;
        // In the constructor of the BurialListController class, an instance of the postgresContext class is injected, which is used for interacting with the database. 

        public BurialListController(postgresContext context)
        {
            _context = context;
        }
        // The Index method of the BurialListController class is an asynchronous method that handles HTTP GET requests for the burials list. 
        // It has several optional parameters, including pageNumber, pageNumberFilter, pageNumberBurial, sortOrder, and search. 
        // The Index method uses ViewData to store and pass data to the view, including the current sorting order.
        public async Task<IActionResult> Index(int? pageNumber, int? pageNumberFilter, int? pageNumberBurial, string sortOrder, userSearch? search = null)
        {
            ViewData["CurrentSort"] = sortOrder;
            // It also sets the default number of items to display per page to 6. 

            int pageSize = 6;
            // If the search parameter has values for TextileColor, TextileFunction, or TextileStructure, it filters the burials list by these textile properties. 

            if (search.TextileColor != null || search.TextileFunction != null || search.TextileStructure != null)
            {
                //Filter tables together. This code path works as in inner join when a filter is placed on one of the textile tables, only those burials with associalted textiles are returned
                var burialmainTextile = _context.BurialmainTextiles.Include(x => x.Burialmain).Include(x => x.Textile);

                var viewmodel = burialmainTextile //Select all data for view model
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
                        //The tables have a many to many relationship so this gets only the associated rows of the entities
                        textileColor = _context.ColorTextiles
                            .Include(p => p.Textile)
                            .Include(p => p.Color)
                            .Where(p => p.MainTextileid == x.MainTextileid)                                              
                            .ToList(),
                        textileStructure = _context.StructureTextiles
                            .Include(p => p.Textile)
                            .Include(p => p.Structure)
                            .Where(p => p.MainTextileid == x.MainTextileid)                                            
                            .ToList(),
                        textileFunction = _context.TextilefunctionTextiles
                            .Include(p => p.Textile)
                            .Include(p => p.Textilefunction)
                            .Where(p => p.MainTextileid == x.MainTextileid)                                               
                            .ToList()
                    }) //Filter results based on conditions received from user
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
                        (search.TextileColor == null || items.textileColor.Any(q => q.Color.Value.Contains(search.TextileColor))) &&
                        (search.TextileFunction == null || items.textileFunction.Any(q => q.Textilefunction.Value.Contains(search.TextileFunction))) &&
                        (search.TextileStructure == null || items.textileStructure.Any(q => q.Structure.Value.Contains(search.TextileStructure)))
                        );

                var textileColorList = await _context.Colors.Select(x => x.Value).Distinct().ToListAsync();
                var textileStructureList = await _context.Structures.Select(x => x.Value).Distinct().ToListAsync();
                var textileFunctionList = await _context.Textilefunctions.Select(x => x.Value).Distinct().ToListAsync();
                
                var viewModel = new filterViewModel
                {
                    //Create the paginated list from the inputted models
                    displayBurial = await PaginatedList<idknameyet>.CreateAsync(viewmodel.AsNoTracking(), pageNumber ?? 1, pageSize),
                    //set user search to the inputted values so they stay in the form on resubmission
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
                        Text = search.Text,

                        AgeList = await _context.Burialmains.Select(x => x.Ageatdeath).Distinct().ToListAsync(),
                        HairColorList = await _context.Burialmains.Select(x => x.Haircolor).Distinct().ToListAsync(),
                        SexList = await _context.Burialmains.Select(x => x.Sex).Distinct().ToListAsync(),
                        HeadDirectionList = await _context.Burialmains.Select(x => x.Headdirection).Distinct().ToListAsync(),
                        TextileColorList = textileColorList.Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() }).ToList(),
                        TextileStructureList = textileStructureList.Select(s => new SelectListItem { Value = s.ToString(), Text = s.ToString() }).ToList(),
                        TextileFunctionList = textileFunctionList.Select(f => new SelectListItem { Value = f.ToString(), Text = f.ToString() }).ToList()
                    }
                };

                return viewModel != null ?
                            View("OldTable", viewModel) :
                            Problem("Entity set 'postgresContext.Burialmains'  is null.");

            }
           
            else
            {
                //Filter tables separate. This view model functions as a left join where all burials are retuened whether or not they have a textile
                //Select all data for view model
                var burialmains = _context.Burialmains.Select(x => new displayBurial
                {
                    Id = x.Id,
                    Squarenorthsouth = x.Squarenorthsouth,
                    Northsouth = x.Northsouth,
                    Squareeastwest = x.Squareeastwest,
                    Eastwest = x.Eastwest,
                    Area = x.Area,
                    AgeAtDeath = x.Ageatdeath,
                    BurialNumber = x.Burialnumber,
                    Depth = x.Depth,
                    HairColor = x.Haircolor,
                    HeadDirection = x.Headdirection,
                    FaceBundles = x.Facebundles,
                    FieldBookExcavationYear = x.Fieldbookexcavationyear,
                    Sex = x.Sex,
                    Text = x.Text,

                })//Filter data accordingly
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
                    (string.IsNullOrEmpty(search.Text) || items.Text.Contains(search.Text))
                    );

                var textileColorList = await _context.Colors.Select(x => x.Value).Distinct().ToListAsync();
                var textileStructureList = await _context.Structures.Select(x => x.Value).Distinct().ToListAsync();
                var textileFunctionList = await _context.Textilefunctions.Select(x => x.Value).Distinct().ToListAsync();
                var burialmainTextile = _context.BurialmainTextiles.Include(x => x.Burialmain).Include(x => x.Textile);

                //Set individual tables that match with burials
                var filterLinkingTables = burialmainTextile
                    .Select(x => new filterLinkingTables
                    {
                        textileColor = _context.ColorTextiles
                            .Include(p => p.Textile)
                            .Include(p => p.Color)
                            .Where(p => p.MainTextileid == x.MainTextileid)                                              
                            .ToList(),
                        textileStructure = _context.StructureTextiles
                            .Include(p => p.Textile)
                            .Include(p => p.Structure)
                            .Where(p => p.MainTextileid == x.MainTextileid)                                               
                            .ToList(),
                        textileFunction = _context.TextilefunctionTextiles
                            .Include(p => p.Textile)
                            .Include(p => p.Textilefunction)
                            .Where(p => p.MainTextileid == x.MainTextileid)                                                
                            .ToList()
                    }).Where(items =>
                       (search.TextileColor == null || items.textileColor.Any(q => q.Color.Value.Contains(search.TextileColor))) &&
                        (search.TextileFunction == null || items.textileFunction.Any(q => q.Textilefunction.Value.Contains(search.TextileFunction))) &&
                        (search.TextileStructure == null || items.textileStructure.Any(q => q.Structure.Value.Contains(search.TextileStructure)))
                    );


                var viewModel = new AdvancedFilterViewModel
                {
                    burialmains = await PaginatedList<displayBurial>.CreateAsync(burialmains.AsNoTracking(), pageNumberBurial ?? 1, pageSize),
                    filterLinkingTables = await PaginatedList<filterLinkingTables>.CreateAsync(filterLinkingTables.AsNoTracking(), pageNumberFilter ?? 1, pageSize),
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
                        Text = search.Text,

                        AgeList = await _context.Burialmains.Select(x => x.Ageatdeath).Distinct().ToListAsync(),
                        HairColorList = await _context.Burialmains.Select(x => x.Haircolor).Distinct().ToListAsync(),
                        SexList = await _context.Burialmains.Select(x => x.Sex).Distinct().ToListAsync(),
                        HeadDirectionList = await _context.Burialmains.Select(x => x.Headdirection).Distinct().ToListAsync(),
                        TextileColorList = textileColorList.Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() }).ToList(),
                        TextileStructureList = textileStructureList.Select(s => new SelectListItem { Value = s.ToString(), Text = s.ToString() }).ToList(),
                        TextileFunctionList = textileFunctionList.Select(f => new SelectListItem { Value = f.ToString(), Text = f.ToString() }).ToList()
                    }

                };
                return viewModel != null ?
                            View("NewTable", viewModel) :
                            Problem("Entity set 'postgresContext.Burialmains'  is null.");

            }  

        }

        // Forward the details page to the cleaned data controller
        public async Task<IActionResult> Details(string id)
        {
            return RedirectToAction("Details", "DetailsPage", new { id = id});
        }
    }
}
