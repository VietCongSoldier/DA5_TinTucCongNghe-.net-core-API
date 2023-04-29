using System;
using System.Collections.Generic;

#nullable disable

namespace DA5_API_TTCN.Models
{
    public partial class Feedback
    {
        public int Feedbackid { get; set; }
        public int? Newsid { get; set; }
        public string Email { get; set; }
        public string Namereader { get; set; }
        public string Contents { get; set; }
        public string Status { get; set; }
        public DateTime? Datecomment { get; set; }
        public bool? Isdeleted { get; set; }
        public string Img { get; set; }
    }
}
