using System;
using System.Collections.Generic;

#nullable disable

namespace DA5_API_TTCN.Models
{
    public partial class Image
    {
        public int Imageid { get; set; }
        public int Newsid { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public bool? Isdeleted { get; set; }
    }
}
