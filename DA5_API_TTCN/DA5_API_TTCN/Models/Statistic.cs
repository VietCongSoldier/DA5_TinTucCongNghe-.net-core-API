using System;
using System.Collections.Generic;

#nullable disable

namespace DA5_API_TTCN.Models
{
    public partial class Statistic
    {
        public int Statisticid { get; set; }
        public DateTime? Posttimenews { get; set; }
        public string Postmostread { get; set; }
        public long? Visitnumer { get; set; }
        public bool? Isdeleted { get; set; }
    }
}
