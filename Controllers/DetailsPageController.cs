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
    public class DetailsPage : Controller
    {
        private readonly postgresContext _context;

        public DetailsPage(postgresContext context)
        {
            _context = context;
        }


        // GET: Detailscleaneddatum/Details/5
        public async Task<IActionResult> Details(string id)
        {
            long ID = long.Parse(id);
            if (id == null || _context.BurialmainTextiles == null)
            {
                return NotFound();
            }
            var burialmainTextileSingle = await _context.BurialmainTextiles
                .Include(p => p.Burialmain)
                .Include(p => p.Textile)
                .Where(x => x.Burialmain.Id == 19140298416326423)
                .FirstAsync();

            var burialmainTextile = await _context.BurialmainTextiles
                .Include(p => p.Burialmain)
                .Include(p => p.Textile)
                .Where(x => x.Burialmain.Id == 19140298416326423)
                .ToListAsync();

            var textileIds = burialmainTextile.Select(x => x.Textile.Id);

            var analysisTextile = await _context.AnalysisTextiles
                .Include(p => p.Analysis)
                .Include(p => p.Textile)
                .Where(x => textileIds.Contains(x.Textile.Id))
                .ToListAsync();

            var colorTextile = await _context.ColorTextiles
                .Include(p => p.Color)
                .Include(p => p.Textile)
                .Where(x => textileIds.Contains(x.Textile.Id))
                .ToListAsync();

            var decorationTextile = await _context.DecorationTextiles
                .Include(p => p.Decoration)
                .Include(p => p.Textile)
                .Where(x => textileIds.Contains(x.Textile.Id))
                .ToListAsync();

            var dimensionTextile = await _context.DimensionTextiles
                .Include(p => p.Dimension)
                .Include(p => p.Textile)
                .Where(x => textileIds.Contains(x.Textile.Id))
                .ToListAsync();

            var textilefunctionTextile = await _context.TextilefunctionTextiles
                .Include(p => p.Textilefunction)
                .Include(p => p.Textile)
                .Where(x => textileIds.Contains(x.Textile.Id))
                .ToListAsync();

            var structureTextile = await _context.StructureTextiles
                .Include(p => p.Structure)
                .Include(p => p.Textile)
                .Where(x => textileIds.Contains(x.Textile.Id))
                .ToListAsync();

            var yarnmanipulationTextile = await _context.YarnmanipulationTextiles
                .Include(p => p.Yarnmanipulation)
                .Include(p => p.Textile)
                .Where(x => textileIds.Contains(x.Textile.Id))
                .ToListAsync();

            var allData = new AllLinkingTablesViewModel
            {
                BurialmainSingle = burialmainTextileSingle,
                BurialmainTextile = burialmainTextile,
                AnalysisTextile = analysisTextile,
                ColorTextile = colorTextile,
                DecorationTextile = decorationTextile,
                DimensionTextile = dimensionTextile,
                TextilefunctionTextile = textilefunctionTextile,
                StructureTextile = structureTextile,
                YarnmanipulationTextile = yarnmanipulationTextile
            };
          
            if (allData == null)
            {
                return NotFound();
            }

            return allData != null ?
                        View(allData) :
                        Problem("Entity set 'postgresContext.Burialmains'  is null.");
        }
    }
}