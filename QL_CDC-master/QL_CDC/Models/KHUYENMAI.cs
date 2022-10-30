using System;
using System.Collections.Generic;

#nullable disable

namespace QL_CDC.Models
{
    public partial class KHUYENMAI
    {
        public string KM_MSKM { get; set; }
        public string SP_MSSP { get; set; }
        public double? KM_PHANTRAM { get; set; }

        public virtual SANPHAM SP_MSSPNavigation { get; set; }
    }
}
