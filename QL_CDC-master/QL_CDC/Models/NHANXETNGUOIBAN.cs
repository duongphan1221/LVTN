using System;
using System.Collections.Generic;

#nullable disable

namespace QL_CDC.Models
{
    public partial class NHANXETNGUOIBAN
    {
        public string SV_MSSV_M { get; set; }
        public string SV_MSSV_B { get; set; }
        public string NX_NOIDUNG { get; set; }
        public int? NX_GIATRI { get; set; }
        public DateTime? NX_NGAY { get; set; }

        public virtual SINHVIEN SV_MSSV_BNavigation { get; set; }
        public virtual SINHVIEN SV_MSSV_MNavigation { get; set; }
    }
}
