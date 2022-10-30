using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QL_CDC.Models
{
    public class HoaDonModel
    {
        public string MaHoaDon { get; set; }
        public SinhVienModel NguoiBan { get; set; }
        public GioHangModel GioHang { get; set; }
        public List<SanPhamModel> SanPham { get; set; }
        public string TinhTrang { get; set; }
        public int MaTinhTrang { get; set; }
        public DateTime NgayMua { get; set; }
        public double TongGia { get; set; }
        public double TienShip { get; set; }
    }

    public class TongThanhToan
    {
        public List<HoaDonModel> HD { get; set; }
        public double TongGia { get; set; }
        public SinhVienModel NguoiMua { get; set; }
    }
}
