using System;
using System.Collections.Generic;

#nullable disable

namespace QL_CDC.Models
{
    public partial class LOAIMATHANG
    {
        public LOAIMATHANG()
        {
            LOAISANPHAMs = new HashSet<LOAISANPHAM>();
        }

        public int MH_MAMH { get; set; }
        public string MH_TENMH { get; set; }

        public virtual ICollection<LOAISANPHAM> LOAISANPHAMs { get; set; }
    }
}
