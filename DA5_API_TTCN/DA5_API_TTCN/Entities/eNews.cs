using System;
using System.Collections.Generic;

namespace DA5_API_TTCN.Entities
{
    public class eNews
    {
        public int? Newsid { get; set; }
        public string CategoryName { get; set; }
        public int Categoryid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int Authorid { get; set; }
        public string Fullname { get; set; }
        public DateTime? Posttime { get; set; }
        public string Keyword { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public int? Numread { get; set; }
        public bool? Isdeleted { get; set; }
    }
}
