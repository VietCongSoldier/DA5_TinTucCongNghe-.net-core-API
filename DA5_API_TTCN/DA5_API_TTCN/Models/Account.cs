using System;
using System.Collections.Generic;

#nullable disable

namespace DA5_API_TTCN.Models
{
    public partial class Account
    {
        public int Accountid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Memberid { get; set; }
        public string Decentralization { get; set; }
        public DateTime? Registrationdate { get; set; }
        public string Statusmem { get; set; }
        public bool? Isdeleted { get; set; }
    }
}
