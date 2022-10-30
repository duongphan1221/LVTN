using System;
using System.Collections.Generic;

#nullable disable

namespace QL_CDC.Models
{
    public partial class GIOHANG
    {
        public string SV_MSSV { get; set; }
        public string SP_MSSP { get; set; }
        public int? GH_SOLUONG { get; set; }

        public virtual SANPHAM SP_MSSPNavigation { get; set; }
        public virtual SINHVIEN SV_MSSVNavigation { get; set; }
    }
}
