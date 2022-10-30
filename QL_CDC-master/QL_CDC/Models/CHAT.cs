using System;
using System.Collections.Generic;

#nullable disable

namespace QL_CDC.Models
{
    public partial class CHAT
    {
        public string SV_MSSV_G { get; set; }
        public string SV_MSSV_N { get; set; }
        public DateTime CHAT_THOIGIAN { get; set; }
        public string CHAT_NOIDUNG { get; set; }
        public bool? CHAT_DADOC { get; set; }

        public virtual SINHVIEN SV_MSSV_GNavigation { get; set; }
        public virtual SINHVIEN SV_MSSV_NNavigation { get; set; }
    }
}
