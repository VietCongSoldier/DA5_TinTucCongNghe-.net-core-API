using System;
using System.Collections.Generic;

#nullable disable

namespace DA5_API_TTCN.Models
{
    public partial class Member
    {
        public int Memberid { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phonenumber { get; set; }
        public bool? Isdeleted { get; set; }
        public string Img { get; set; }
    }
}
