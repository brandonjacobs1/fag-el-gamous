using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class C14
    {
        public int Sample { get; set; }
        public int? Squarenorthsouth { get; set; }
        public string? Northsouth { get; set; }
        public int? Squareeastwest { get; set; }
        public string? Eastwest { get; set; }
        public string? Area { get; set; }
        public int? Burialnumber { get; set; }
        public string? Description { get; set; }
        public int? Agebp { get; set; }
        public string Calendardate { get; set; } = null!;
    }
}
