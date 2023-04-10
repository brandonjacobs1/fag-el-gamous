using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class Newsarticle
    {
        public long Id { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Urltoimage { get; set; }
        public string? Url { get; set; }
    }
}
