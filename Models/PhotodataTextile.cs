using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class PhotodataTextile
    {
        public long MainPhotodataid { get; set; }
        public Photodatum Photodatum { get; set; }
        public long MainTextileid { get; set; }
        public Textile Textile { get; set; }
    }
}
