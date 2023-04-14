using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace fag_el_gamous.Models
{
	public class userSearch
	{
        
        public string? LocationString { get; set; }
        public string? Sex { get; set; }
        public float? MinDepth { get; set; }
        public float? MaxDepth { get; set; }
        public string? AgeAtDeath { get; set; }
        public string? HairColor { get; set; }
        public string? HeadDirection { get; set; }
        public string? TextileColor { get; set; }
        public List<string>? TextileStructure { get; set; }
        public List<string>? TextileFunction { get; set; }
        public string? FaceBundles { get; set; }
        public string? Text { get; set; }
        public string? FieldBookExcavationYear { get; set; }

        public List<string?>? SexList { get; set; }
        public List<string?>? HeadDirectionList { get; set; }
        public List<SelectListItem>? TextileColorList { get; set; }
        public List<SelectListItem>? TextileStructureList { get; set; }
        public List<SelectListItem>? TextileFunctionList { get; set; }
        //public decimal? minEstimateStature { get; set; }//num 
        //public decimal? maxEstimateStature { get; set; }//num
    }
}

