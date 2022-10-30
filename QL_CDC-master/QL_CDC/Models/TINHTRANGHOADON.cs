using System;
using System.Collections.Generic;

#nullable disable

namespace QL_CDC.Models
{
    public partial class TINHTRANGHOADON
    {
        public TINHTRANGHOADON()
        {
            HOADONMUAs = new HashSet<HOADONMUA>();
        }

        public int TT_MSTT { get; set; }
        public string TT_TRANGTHAI { get; set; }

        public virtual ICollection<HOADONMUA> HOADONMUAs { get; set; }
    }
}
