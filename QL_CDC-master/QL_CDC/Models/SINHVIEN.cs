using System;
using System.Collections.Generic;

#nullable disable

namespace QL_CDC.Models
{
    public partial class SINHVIEN
    {
        public SINHVIEN()
        {
            CHATSV_MSSV_GNavigations = new HashSet<CHAT>();
            CHATSV_MSSV_NNavigations = new HashSet<CHAT>();
            GIOHANGs = new HashSet<GIOHANG>();
            HINHANHs = new HashSet<HINHANH>();
            HOADONMUAs = new HashSet<HOADONMUA>();
            NHANXETNGUOIBANSV_MSSV_BNavigations = new HashSet<NHANXETNGUOIBAN>();
            NHANXETNGUOIBANSV_MSSV_MNavigations = new HashSet<NHANXETNGUOIBAN>();
            SANPHAMs = new HashSet<SANPHAM>();
        }

        public string SV_MSSV { get; set; }
        public string SV_MATKHAU { get; set; }
        public string SV_HOTEN { get; set; }
        public string SV_TENHIENTHI { get; set; }
        public DateTime? SV_NGAYTAOTK { get; set; }
        public DateTime? SV_LANHDCUOI { get; set; }
        public string SV_DIACHIGIAOHANG { get; set; }
        public string SV_SDT { get; set; }
        public string SV_EMAIL { get; set; }
        public bool? SV_TINHTRANG { get; set; }
        public bool? SV_ADMIN { get; set; }

        public virtual ICollection<CHAT> CHATSV_MSSV_GNavigations { get; set; }
        public virtual ICollection<CHAT> CHATSV_MSSV_NNavigations { get; set; }
        public virtual ICollection<GIOHANG> GIOHANGs { get; set; }
        public virtual ICollection<HINHANH> HINHANHs { get; set; }
        public virtual ICollection<HOADONMUA> HOADONMUAs { get; set; }
        public virtual ICollection<NHANXETNGUOIBAN> NHANXETNGUOIBANSV_MSSV_BNavigations { get; set; }
        public virtual ICollection<NHANXETNGUOIBAN> NHANXETNGUOIBANSV_MSSV_MNavigations { get; set; }
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
