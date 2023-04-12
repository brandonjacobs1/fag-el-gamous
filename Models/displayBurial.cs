using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace fag_el_gamous.Models
{
	public class displayBurial
	{
		[Key]
		public string Id { get; set; }
        public int Squarenorthsouth { get; set; }
        public string? Northsouth { get; set; }
        public int Squareeastwest { get; set; }
        public string? Eastwest { get; set; }
        public string? Area { get; set; }
        public string? burialNumber { get; set; }
        public string? locationString
        {
            get
            {
                return $"{Squarenorthsouth}{Northsouth} {Squareeastwest}{Eastwest} {Area} #{burialNumber}";
            }
        }
        public float? depth { get; set; }
        public string? ageAtDeath { get; set; }
        public string? hairColor { get; set; }
        public string? fieldBookExcavationYear { get; set; }
        public string? sex { get; set; }
        public string? headDirection { get; set; }
        public string? textileStructure { get; set; }
        public string? textileFunction { get; set; }
        public bool? Robust { get; set; }
        public bool? ParietalBlossing { get; set; }
        public string? estimateStature { get; set; }

    }
}

