using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace fag_el_gamous.Models
{
	public class userSearch
	{
            [StringLength(50)]
            public string? LocationString { get; set; }
            [RegularExpression("^(M|F)$")]
            public string? Sex { get; set; }
            [Range(0, 10)]
            public float? MinDepth { get; set; }
            [Range(0, 10)]
            public float? MaxDepth { get; set; }
            [RegularExpression("^(I|C|N|A)$")]
            public string? AgeAtDeath { get; set; }
            public string? HairColor { get; set; }
            [RegularExpression("^(N|E|S|W|U)$")]
            public string? HeadDirection { get; set; }
            public string? TextileColor { get; set; }
            public string? TextileStructure { get; set; }
            public string? TextileFunction { get; set; }
            [RegularExpression("^(Y|N)$")]
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

