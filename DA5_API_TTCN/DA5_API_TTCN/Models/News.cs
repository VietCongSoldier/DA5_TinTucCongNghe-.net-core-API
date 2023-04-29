using System;
using System.Collections.Generic;

#nullable disable

namespace DA5_API_TTCN.Models
{
    public partial class News
    {
        public int Newsid { get; set; }
        public int Categoryid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int Authorid { get; set; }
        public DateTime? Posttime { get; set; }
        public string Keyword { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public int? Numread { get; set; }
        public bool? Isdeleted { get; set; }
    }
}
