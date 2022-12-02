using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QL_CDC.Models
{
    public class NhanXetModel
    {
        public string mssv_m { get; set; }
        public string mssv_b { get; set; }
        public string img { get; set; }
        public string noidung { get; set; }
        public int danhgia { get; set; }
        public string ngay { get; set; }
    }
    public class SanPhamModel
    {
        public string masp { get; set; }
        public string tensp { get; set; }
        public int maloai { get; set; }
        public string anhdaidien { get; set; }
        public int mamh { get; set; }
        public List<string> anhsp { get; set; }
        public double giagocsp { get; set; }
        public double dongiasp { get; set; }
        public int thoigiansp { get; set; }
        public double danhgiasp { get; set; }
        public int soluongsp { get; set; }
        public string nguoidangsp { get; set; }
        public string msnguoidang { get; set; }
        public string ngaydangsp { get; set; }
        public string motasp { get; set; }
        public string makm { get; set; }
        public int luotxemsp { get; set; }
        public string nsx { get; set; }
        public string loai { get; set; }
        public List<NhanXetModel> nhanxetsp { get; set; }
        public List<IFormFile> img { get; set; } 

        public bool tinhtrang { get; set; }

        public string tt { get; set; }

        public int mh1 { get; set; }
        public int mh2 { get; set; }
        public int mh3 { get; set; }
        public int mh4 { get; set; }
        public int mh5 { get; set; }

        public string sdt { get; set; }
    }
}
