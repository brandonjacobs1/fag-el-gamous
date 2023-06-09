﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace fag_el_gamous.Models
{
	public class displayBurial
	{
        [Key]
        public long? Id { get; set; }
        public string? Squarenorthsouth { get; set; }
        public string? Northsouth { get; set; }
        public string? Squareeastwest { get; set; }
        public string? Eastwest { get; set; }
        public string? Area { get; set; }
        public string? BurialNumber { get; set; }
        public string? Text { get; set; }
        public string? LocationString
        {
            get
            {
                return $"{Squarenorthsouth}{Northsouth} {Squareeastwest}{Eastwest} {Area} #{BurialNumber}";
            }
        }
        public float? Depth { get; set; }
        public string? AgeAtDeath { get; set; }
        public string? HairColor { get; set; }
        public string? FieldBookExcavationYear { get; set; }
        public string? Sex { get; set; }
        public string? HeadDirection { get; set; }
        public string? FaceBundles { get; set; }

    }
}

