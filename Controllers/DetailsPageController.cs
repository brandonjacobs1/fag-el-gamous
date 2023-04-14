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
            //string bodyAnalysisId = "19140298416326114";
            //string Photoid = "19140298416326447";
            //id = Photoid;
            long ID = long.Parse(id);

            if (id == null || _context.BurialmainTextiles == null)
            {
                return NotFound();
            }
            var burialMainNoAssociation = await _context.Burialmains.Where(x => x.Id == ID).FirstOrDefaultAsync() ?? new Burialmain();
            
            var burialmainTextileSingle = await _context.BurialmainTextiles
                .Include(p => p.Burialmain)
                .Include(p => p.Textile)
                .Where(x => x.Burialmain.Id == ID)
                .FirstOrDefaultAsync() ?? new BurialmainTextile();

            var burialmainTextile = await _context.BurialmainTextiles
                .Include(p => p.Burialmain)
                .Include(p => p.Textile)
                .Where(x => x.Burialmain.Id == ID)
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

            var photodataTextile = await _context.PhotodataTextiles
                .Include(p => p.Photodatum)
                .Include(p => p.Textile)
                .Where(x => textileIds.Contains(x.Textile.Id))
                .ToListAsync();

            var bodyAnalysisChartViewModel = setBodyAnalysis(ID);
            var allData = new AllLinkingTablesViewModel
            {
                BurialMainNoAssociation = burialMainNoAssociation,
                BurialmainSingle = burialmainTextileSingle,
                BurialmainTextile = burialmainTextile,
                AnalysisTextile = analysisTextile,
                ColorTextile = colorTextile,
                DecorationTextile = decorationTextile,
                DimensionTextile = dimensionTextile,
                TextilefunctionTextile = textilefunctionTextile,
                StructureTextile = structureTextile,
                YarnmanipulationTextile = yarnmanipulationTextile,
                PhotodataTextile = photodataTextile,
                BodyAnalysisViewModel = bodyAnalysisChartViewModel
            };
          
            if (allData == null)
            {
                return NotFound();
            }
            

            return allData != null ?
                        View(allData) :
                        Problem("Entity set 'postgresContext.Burialmains'  is null.");
        }

        public  BodyAnalysisChartViewModel setBodyAnalysis(long? id)
        {
            var data =  _context.BodyanalysischartBurialmains.Where(x => x.Id == id).FirstOrDefault();
            var bodyanalysischart = data == null ? new BodyAnalysisChartViewModel() :
            new BodyAnalysisChartViewModel
            {

                Id = data.Id,
                Dateofexamination = data.Dateofexamination,
                Preservationindex = data.Preservationindex,
                Observations = data.Observations,
                Robust = data.Robust,
                Supraorbitalridges = data.Supraorbitalridges,
                Orbitedge = data.Orbitedge,
                Parietalbossing = data.Parietalbossing,
                Gonion = data.Gonion,
                Nuchalcrest = data.Nuchalcrest,
                Zygomaticcrest = data.Zygomaticcrest,
                Sphenooccipitalsynchrondrosis = data.Sphenooccipitalsynchrondrosis,
                Lamboidsuture = data.Lamboidsuture,
                Squamossuture = data.Squamossuture,
                Toothattrition = data.Toothattrition,
                Tootheruption = data.Tootheruption,
                Tootheruptionageestimate = data.Tootheruptionageestimate,
                Ventralarc = data.Ventralarc,
                Subpubicangle = data.Subpubicangle,
                Sciaticnotch = data.Sciaticnotch,
                Pubicbone = data.Pubicbone,
                Preauricularsulcus = data.PreauricularsulcusBoolean,
                MedialIpRamus = data.MedialIpRamus,
                Dorsalpitting = data.DorsalpittingBoolean,
                Femur = data.Femur,
                Humerus = data.Humerus,
                Femurheaddiameter = data.Femurheaddiameter,
                Humerusheaddiameter = data.Humerusheaddiameter,
                Femurlength = data.Femurlength,
                Humeruslength = data.Humeruslength,
                Estimatestature = data.Estimatestature,
                Osteophytosis = data.Osteophytosis,
                CariesPeriodontalDisease = data.CariesPeriodontalDisease,
                Notes = data.Notes
            };
            return bodyanalysischart;
        }
    }
}