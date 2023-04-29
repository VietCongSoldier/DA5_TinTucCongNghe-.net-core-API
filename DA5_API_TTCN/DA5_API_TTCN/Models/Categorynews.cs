using System;
using System.Collections.Generic;

#nullable disable

namespace DA5_API_TTCN.Models
{
    public partial class Categorynews
    {
        public int CategorynewsId { get; set; }
        public string Categorynewsname { get; set; }
        public string Noidung { get; set; }
        public bool? Isdeleted { get; set; }
        public int? Sapxep { get; set; }
        public int? Visiblemenu { get; set; }
        public string Url { get; set; }
        public string Linkngoai { get; set; }
    }
}
