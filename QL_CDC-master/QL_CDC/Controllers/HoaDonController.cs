using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QL_CDC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QL_CDC.Controllers
{
    public class HoaDonController : Controller
    {
        QL_CDCContext db = new QL_CDCContext();

        [Authorize]
        public IActionResult HoaDon()
        {
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (db.GIOHANGs.Where(a => a.SV_MSSV == mssv).Count() == 0)
            {
                return RedirectToAction("GioHang", "GioHang");
            }
            else
            {
                TongThanhToan T = new TongThanhToan();
                T.HD = new List<HoaDonModel>();
                T.TongGia = 0;
                foreach (var nb in db.GIOHANGs.Where(a => a.SV_MSSV == mssv).Select(a => a.SP_MSSPNavigation.SV_MSSV).Distinct())
                {
                    HoaDonModel HD = new HoaDonModel();
                    GioHangModel GH = new GioHangModel();
                    GH.TongGia = 0;
                    GH.SoLoai = 0;
                    GH.SanPham = new List<HangModel>();
                    int count = 0;
                    foreach (var i in db.GIOHANGs.Where(a => a.SV_MSSV == mssv && a.SP_MSSPNavigation.SV_MSSV == nb))
                    {
                        SANPHAM sp = db.SANPHAMs.Where(a => a.SP_MSSP == i.SP_MSSP).FirstOrDefault();
                        HangModel m = new HangModel();
                        m.stt = count + 1;
                        m.masp = i.SP_MSSP;
                        m.tensp = sp.SP_TENSP;
                        m.gia = (double)TinhDonGiaSanPham(i.SP_MSSP);
                        m.soluong = (int)i.GH_SOLUONG;
                        m.tonggia = m.soluong * m.gia;
                        m.conlai = (int)sp.SP_CONLAI;
                        m.img = db.HINHANHs.Where(a => a.SP_MSSP == i.SP_MSSP).Select(a => a.HA_LINK).FirstOrDefault();
                        GH.SanPham.Add(m);
                        GH.TongGia += m.tonggia;
                        GH.SoLoai += 1;
                        count++;
                    }
                    HD.GioHang = new GioHangModel();
                    HD.GioHang = GH;
                    HD.NguoiBan = new SinhVienModel()
                    {
                        MSSV = nb,
                        HoTen = db.SINHVIENs.Where(a => a.SV_MSSV == nb).Select(a => a.SV_TENHIENTHI).FirstOrDefault(),
                    };
                    HD.TienShip = TinhTienShip(HD.GioHang);
                    HD.TongGia = GH.TongGia + HD.TienShip;
                    HD.MaHoaDon = Guid.NewGuid().ToString();
                    T.HD.Add(HD);
                    T.TongGia += HD.TongGia;
                }

                SINHVIEN sv = db.SINHVIENs.Where(a => a.SV_MSSV == mssv).FirstOrDefault();
                T.NguoiMua = new SinhVienModel()
                {
                    MSSV = sv.SV_MSSV,
                    HoTen = sv.SV_HOTEN,
                    DiaChi = sv.SV_DIACHIGIAOHANG,
                    SDT = sv.SV_SDT,
                    Email = sv.SV_EMAIL,
                };

                TempData["ThanhToan"] = JsonConvert.SerializeObject(T);
                return View(T);
            }
        }

        public double TinhTienShip(GioHangModel G)
        {
            double rp = 0;
            List<int> maship = new List<int>();
            foreach(var i in G.SanPham.Select(a => a.masp))
            {
                SANPHAM S = db.SANPHAMs.Where(a => a.SP_MSSP == i).FirstOrDefault();
                LOAISANPHAM L = db.LOAISANPHAMs.Where(a => a.LOAI_MALOAI == S.LOAI_MALOAI).FirstOrDefault();
                TIENSHIP T = db.TIENSHIPs.Where(a => a.SHIP_MA == L.SHIP_MA).FirstOrDefault();
                maship.Add(T.SHIP_MA);
            }
            maship = (List<int>)maship.Distinct().ToList();
            foreach(var i in maship)
            {
                rp += (double)db.TIENSHIPs.Where(a => a.SHIP_MA == i).Select(a => a.SHIP_GIA).FirstOrDefault();
            }
            return rp;
        }

        public double TinhDonGiaSanPham(string mssp)
        {
            double dongia = 0;
            double phantram = 1;
            if (db.KHUYENMAIs.Where(a => a.SP_MSSP == mssp).FirstOrDefault() != null)
            {
                phantram = (double)db.KHUYENMAIs.Where(a => a.SP_MSSP == mssp).Select(a => a.KM_PHANTRAM).FirstOrDefault();
            }
            double giagoc = (double)db.SANPHAMs.Where(a => a.SP_MSSP == mssp).Select(a => a.SP_GIA).FirstOrDefault();
            dongia = giagoc * phantram;
            return dongia;
        }

        public IActionResult XacNhanHoaDon()
        {
            if(TempData["ThanhToan"] != null)
            {
                var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var str = TempData["ThanhToan"].ToString();
                TongThanhToan T = JsonConvert.DeserializeObject<TongThanhToan>(str);
                foreach(var hd in T.HD)
                {
                    HOADONMUA HDM = new HOADONMUA()
                    {
                        HD_MSHD = hd.MaHoaDon,
                        SV_MSSV = mssv,
                        TT_MSTT = 1,
                        HD_NGAYMUA = DateTime.Now,
                        HD_TONGGIA = hd.TongGia,
                        HD_DIACHI = T.NguoiMua.DiaChi,
                        HD_SDT = T.NguoiMua.SDT,
                        HD_NGUOINHAN = T.NguoiMua.HoTen,
                    };
                    db.HOADONMUAs.Add(HDM);
                    
                    foreach(var sp in hd.GioHang.SanPham)
                    {
                        CHITIETHOADON CT = new CHITIETHOADON()
                        {
                            SP_MSSP = sp.masp,
                            HD_MSHD = hd.MaHoaDon,
                            CTHD_SOLUONG = sp.soluong,
                            SP_GIABAN = sp.gia
                        };
                        db.CHITIETHOADONs.Add(CT);

                        GIOHANG GH = db.GIOHANGs.Where(a => a.SV_MSSV == mssv && a.SP_MSSP == sp.masp).FirstOrDefault();
                        db.Remove(GH);
                    }
                }
                db.SaveChanges();

                return RedirectToAction("DanhSachHoaDonNguoiMua", "HoaDon");
            }
            else
            {
                return RedirectToAction("HoaDon");
            }
        }

        [Authorize]
        public IActionResult DanhSachHoaDonNguoiMua()
        {
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<HoaDonModel> HD = new List<HoaDonModel>();
            foreach(var hd in db.HOADONMUAs.Where(a => a.SV_MSSV == mssv))
            {
                HoaDonModel H = new HoaDonModel()
                {
                    MaHoaDon = hd.HD_MSHD,
                    NgayMua = (DateTime)hd.HD_NGAYMUA,
                    TongGia = (double)hd.HD_TONGGIA,
                    TinhTrang = db.TINHTRANGHOADONs.Where(a => a.TT_MSTT == hd.TT_MSTT).Select(a => a.TT_TRANGTHAI).First(),
                    MaTinhTrang = hd.TT_MSTT
                };
                HD.Add(H);
            }
            return View(HD.OrderByDescending(a => a.NgayMua));
        }

        [Authorize]
        public IActionResult ChiTietHoaDonMua(string mahd)
        {
            HoaDonModel model = new HoaDonModel();
            HOADONMUA HDM = db.HOADONMUAs.Where(a => a.HD_MSHD == mahd).FirstOrDefault();
            model.MaHoaDon = HDM.HD_MSHD;
            model.NgayMua = (DateTime)HDM.HD_NGAYMUA;
            model.TinhTrang = db.TINHTRANGHOADONs.Where(a => a.TT_MSTT == HDM.TT_MSTT).Select(a => a.TT_TRANGTHAI).First();
            model.TongGia = (double)HDM.HD_TONGGIA;

            model.SanPham = new List<SanPhamModel>();
            foreach(var item in db.CHITIETHOADONs.Where(a => a.HD_MSHD == HDM.HD_MSHD))
            {
                SANPHAM S = db.SANPHAMs.Where(x => x.SP_MSSP == item.SP_MSSP).FirstOrDefault();
                SanPhamModel sp = new SanPhamModel() { 
                    masp = S.SP_MSSP,
                    anhdaidien = db.HINHANHs.Where(a => a.SP_MSSP == S.SP_MSSP).Select(a => a.HA_LINK).FirstOrDefault(),
                    dongiasp = (double)item.SP_GIABAN,
                    soluongsp = (int)item.CTHD_SOLUONG,
                    tensp = S.SP_TENSP
                };
                model.SanPham.Add(sp);
                model.NguoiBan = new SinhVienModel();
                model.NguoiBan.TenHienThi = db.SINHVIENs.Where(x => x.SV_MSSV == S.SV_MSSV).Select(x => x.SV_TENHIENTHI).FirstOrDefault();
            }

            TongThanhToan T = new TongThanhToan();
            T.HD = new List<HoaDonModel>();
            T.HD.Add(model);
            T.NguoiMua = new SinhVienModel()
            {
                HoTen = HDM.HD_NGUOINHAN,
                DiaChi = HDM.HD_DIACHI,
                SDT = HDM.HD_SDT,
            };

            return View(T);
        }

        [Authorize]
        public IActionResult DanhSachHoaDonNguoiBan()
        {
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<string> mahd = db.CHITIETHOADONs.Where(a => a.SP_MSSPNavigation.SV_MSSV == mssv).Select(a => a.HD_MSHD).Distinct().ToList();
            List<HoaDonModel> HD = new List<HoaDonModel>();
            foreach(var item in mahd)
            {
                HOADONMUA hd = db.HOADONMUAs.Where(a => a.HD_MSHD == item).FirstOrDefault();
                HoaDonModel H = new HoaDonModel()
                {
                    MaHoaDon = hd.HD_MSHD,
                    NgayMua = (DateTime)hd.HD_NGAYMUA,
                    TongGia = (double)hd.HD_TONGGIA,
                    TinhTrang = db.TINHTRANGHOADONs.Where(a => a.TT_MSTT == hd.TT_MSTT).Select(a => a.TT_TRANGTHAI).First(),
                    MaTinhTrang = hd.TT_MSTT,
                };
                HD.Add(H);
            }
            return View(HD.OrderBy(a => a.NgayMua));
        }

        [Authorize]
        public IActionResult ChiTietHoaDonBan(string mahd)
        {
            HoaDonModel model = new HoaDonModel();
            HOADONMUA HDM = db.HOADONMUAs.Where(a => a.HD_MSHD == mahd).FirstOrDefault();
            model.MaHoaDon = HDM.HD_MSHD;
            model.NgayMua = (DateTime)HDM.HD_NGAYMUA;
            model.TinhTrang = db.TINHTRANGHOADONs.Where(a => a.TT_MSTT == HDM.TT_MSTT).Select(a => a.TT_TRANGTHAI).First();
            model.TongGia = (double)HDM.HD_TONGGIA;
            model.MaTinhTrang = HDM.TT_MSTT;

            model.SanPham = new List<SanPhamModel>();
            foreach (var item in db.CHITIETHOADONs.Where(a => a.HD_MSHD == HDM.HD_MSHD))
            {
                SANPHAM S = db.SANPHAMs.Where(x => x.SP_MSSP == item.SP_MSSP).FirstOrDefault();
                SanPhamModel sp = new SanPhamModel()
                {
                    masp = S.SP_MSSP,
                    anhdaidien = db.HINHANHs.Where(a => a.SP_MSSP == S.SP_MSSP).Select(a => a.HA_LINK).FirstOrDefault(),
                    dongiasp = (double)item.SP_GIABAN,
                    soluongsp = (int)item.CTHD_SOLUONG,
                    tensp = S.SP_TENSP
                };
                model.SanPham.Add(sp);
            }

            TongThanhToan T = new TongThanhToan();
            T.HD = new List<HoaDonModel>();
            T.HD.Add(model);
            T.NguoiMua = new SinhVienModel()
            {
                MSSV = HDM.SV_MSSV,
                HoTen = HDM.HD_NGUOINHAN,
                DiaChi = HDM.HD_DIACHI,
                SDT = HDM.HD_SDT,
            };

            return View(T);
        }

        // Dem so luong hoa don chua xac nhan
        public IActionResult DemHoaDonMoi()
        {
            int sl = 0;
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<string> mahd = db.CHITIETHOADONs.Where(a => a.SP_MSSPNavigation.SV_MSSV == mssv).Select(a => a.HD_MSHD).Distinct().ToList();
            foreach(var item in mahd)
            {
                int tt = (int)db.HOADONMUAs.Where(a => a.HD_MSHD == item).Select(a => a.TT_MSTT).FirstOrDefault();
                if (tt == 1)
                {
                    sl += 1;
                }
            }
            return Json(sl);
        }

        // Xac nhan hoa don
        [HttpPost]
        public IActionResult XacNhanHoaDon(string mshd)
        {
            HOADONMUA H = db.HOADONMUAs.Where(a => a.HD_MSHD == mshd).FirstOrDefault();
            int stt = db.TINHTRANGHOADONs.Max(a => a.TT_MSTT);
            db.Entry(H).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            if(H.TT_MSTT < stt)
            {
                H.TT_MSTT += 1;
            }
            db.Entry(H).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Json("Xacnhan");
        }

        // Huy hoa don
        [HttpPost]
        public IActionResult HuyHoaDon(string mshd)
        {
            HOADONMUA H = db.HOADONMUAs.Where(a => a.HD_MSHD == mshd).FirstOrDefault();
            db.Entry(H).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            H.TT_MSTT = 0;
            db.Entry(H).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Json("Huy");
        }
    }
}
