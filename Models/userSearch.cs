using System;
namespace fag_el_gamous.Models
{
	public class userSearch
	{
        //Burialmain
        public string? locationString { get; set; } //string
		public string? sex { get; set; } //cat
		public string? ageAtDeath { get; set; } //cat
        public float? minDepth { get; set; } //num
        public float? maxDepth { get; set; } //num
        public string? hairColor { get; set; } //cat
        public string? headDirection { get; set; }  //cat
        //Structure
        public string? textileStructure { get; set; } //
        //textile function
        public string? textileFunction { get; set; } //
        //bodyanalysischart
		public bool? Robust { get; set; } //bool
		public bool? ParietalBlossing { get; set; } //bool
		public string? estimateStature { get; set; }//num 
        
    }
}

