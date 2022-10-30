using System;
using System.Collections.Generic;

#nullable disable

namespace QL_CDC.Models
{
    public partial class CHITIETHOADON
    {
        public string SP_MSSP { get; set; }
        public string HD_MSHD { get; set; }
        public int? CTHD_SOLUONG { get; set; }
        public double? SP_GIABAN { get; set; }

        public virtual HOADONMUA HD_MSHDNavigation { get; set; }
        public virtual SANPHAM SP_MSSPNavigation { get; set; }
    }
}
