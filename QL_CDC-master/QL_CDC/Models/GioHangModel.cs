using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QL_CDC.Models
{
    public class HangModel
    {
        public int stt { get; set; }
        public string masp { get; set; }
        public string img { get; set; }
        public string tensp { get; set; }
        public double gia { get; set; }
        public int soluong { get; set; }
        public double tonggia { get; set; }
        public int conlai { get; set; }
    }
    public class GioHangModel
    {
        public double TongGia { get; set; }
        public int SoLoai { get; set; }
        public List<HangModel> SanPham { get; set; }
    }
}
