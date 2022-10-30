using System;
using System.Collections.Generic;

#nullable disable

namespace QL_CDC.Models
{
    public partial class TIENSHIP
    {
        public TIENSHIP()
        {
            LOAISANPHAMs = new HashSet<LOAISANPHAM>();
        }

        public int SHIP_MA { get; set; }
        public double? SHIP_GIA { get; set; }

        public virtual ICollection<LOAISANPHAM> LOAISANPHAMs { get; set; }
    }
}
