using System;
using System.Collections.Generic;

#nullable disable

namespace DA5_API_TTCN.Models
{
    public partial class Category
    {
        public int Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Content { get; set; }
        public bool? Isdeleted { get; set; }
        public int? Sort { get; set; }
        public int? Visiblemenu { get; set; }
        public string Url { get; set; }
    }
}
