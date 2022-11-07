using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QL_CDC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QL_CDC.Controllers
{
    public class AdminController : Controller
    {
        QL_CDCContext db = new QL_CDCContext();
        public IActionResult Index()
        {
            List<SinhVienModel> SV = new List<SinhVienModel>();
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var x in db.SINHVIENs)
            {
                if (x.SV_MSSV != mssv)
                {
                    SinhVienModel s = new SinhVienModel();
                    s.MSSV = x.SV_MSSV;
                    s.TenHienThi = x.SV_TENHIENTHI;
                    s.SDT = x.SV_SDT;
                    s.Email = x.SV_EMAIL;
                    s.DiaChi = x.SV_DIACHIGIAOHANG;
                    s.LanHDCuoi = (DateTime)x.SV_LANHDCUOI;
                    if (x.SV_TINHTRANG == true)
                    {
                        s.Khoa = "Khóa người dùng này";
                    }
                    else
                        s.Khoa = "Mở khóa người dùng này";
                    SV.Add(s);
                }
            }
            return View(SV);
        }

        public IActionResult KhoaTaiKhoan(string masv)
        {
            SINHVIEN sv = db.SINHVIENs.Where(a => a.SV_MSSV == masv).FirstOrDefault();
            if (sv.SV_TINHTRANG == true)
            {
                sv.SV_TINHTRANG = false;
            }
            else
                sv.SV_TINHTRANG = true;

            db.SaveChanges();
            return Json(sv.SV_TINHTRANG);

        }

        public IActionResult TinhTrang(string masv)
        {
            SINHVIEN sv = db.SINHVIENs.Where(a => a.SV_MSSV == masv).FirstOrDefault();
            return Json(sv.SV_TINHTRANG);
        }

        // Danh sach bai chua duyet
        public IActionResult DuyetBaiChuaDuyet()
        {
            List<SanPhamModel> SP = new List<SanPhamModel>();
            foreach (var x in db.SANPHAMs)
            {
                if (x.SP_TINHTRANG == false)
                {
                    SanPhamModel s = new SanPhamModel();
                    s.nguoidangsp = getNguoiDangSP(x.SV_MSSV);
                    s.masp = x.SP_MSSP;
                    s.tensp = x.SP_TENSP;
                    s.giagocsp = (double)x.SP_GIA;
                    s.dongiasp = TinhDonGiaSanPham(x.SP_MSSP);
                    s.thoigiansp = (int)x.SP_THOIGIANSUDUNG;
                    s.soluongsp = (int)x.SP_CONLAI;
                    if (x.SP_TINHTRANG == false)
                    {
                        s.tt = "Sản phẩm đang đợi duyệt";
                    }
                    // s.anhsp = db.HINHANHs.Where(a => a.SP_MSSP == x.SP_MSSP).Select(a => a.HA_LINK).ToList();                    

                    SP.Add(s);
                }
            }
            return View(SP);
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
        public string getNguoiDangSP(string masv)
        {
            string nguoidang = db.SINHVIENs.Where(s => s.SV_MSSV == masv).Select(s => s.SV_TENHIENTHI).FirstOrDefault();

            return nguoidang;
        }

        // Hanh dong duyet bai
        public IActionResult DuyetBai(string masp)
        {
            SANPHAM sp = db.SANPHAMs.Where(s => s.SP_MSSP == masp).FirstOrDefault();
            if (sp.SP_TINHTRANG == false)
            {
                sp.SP_TINHTRANG = true;
            }
            db.SaveChanges();
            return Json(sp.SP_TINHTRANG);
        }

        public IActionResult KhongDuyet(string masp)
        {
            SANPHAM sp = db.SANPHAMs.Where(s => s.SP_MSSP == masp).FirstOrDefault();
            if (sp.SP_TINHTRANG == false)
            {
                sp.SP_TINHTRANG = null;
            }
            db.SaveChanges();
            return Json(sp.SP_TINHTRANG);
        }


        // Danh sach bai da duyet
        public IActionResult BaiDaDuyet()
        {
            List<SanPhamModel> SP = new List<SanPhamModel>();
            foreach (var x in db.SANPHAMs)
            {
                if (x.SP_TINHTRANG == true)
                {
                    SanPhamModel s = new SanPhamModel();
                    s.nguoidangsp = getNguoiDangSP(x.SV_MSSV);
                    s.masp = x.SP_MSSP;
                    s.tensp = x.SP_TENSP;
                    s.giagocsp = (double)x.SP_GIA;
                    s.dongiasp = TinhDonGiaSanPham(x.SP_MSSP);
                    s.thoigiansp = (int)x.SP_THOIGIANSUDUNG;
                    s.soluongsp = (int)x.SP_CONLAI;
                    if (x.SP_TINHTRANG == true)
                    {
                        s.tt = "Đã phê duyệt";
                    }
                    // s.anhsp = db.HINHANHs.Where(a => a.SP_MSSP == x.SP_MSSP).Select(a => a.HA_LINK).ToList();                    

                    SP.Add(s);
                }
            }
            return View(SP);
        }

        // Danh sach bai khong duoc duyet
        public IActionResult BaiKhongDuyet()
        {
            List<SanPhamModel> SP = new List<SanPhamModel>();
            foreach (var x in db.SANPHAMs)
            {
                if (x.SP_TINHTRANG == null)
                {
                    SanPhamModel s = new SanPhamModel();
                    s.nguoidangsp = getNguoiDangSP(x.SV_MSSV);
                    s.masp = x.SP_MSSP;
                    s.tensp = x.SP_TENSP;
                    s.giagocsp = (double)x.SP_GIA;
                    s.dongiasp = TinhDonGiaSanPham(x.SP_MSSP);
                    s.thoigiansp = (int)x.SP_THOIGIANSUDUNG;
                    s.soluongsp = (int)x.SP_CONLAI;
                    if (x.SP_TINHTRANG == null)
                    {
                        s.tt = "Không được duyệt";
                    }
                    SP.Add(s);
                }
            }
            return View(SP);
        }

        //Thong ke SP them Loai (Chart)
        public IActionResult ChartTheoLoai()
        {
            return View();
        }

        public IActionResult GetDataChart()
        {
            int loai_1 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 1).Count();
            int loai_2 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 2).Count();
            int loai_3 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 3).Count();
            int loai_4 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 4).Count();
            int loai_5 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 5).Count();
            int loai_6 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 6).Count();
            int loai_7 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 7).Count();
            int loai_8 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 8).Count();
            int loai_9 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 9).Count();
            int loai_10 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 10).Count();
            int loai_11 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 11).Count();
            int loai_12 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 12).Count();
            int loai_13 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 13).Count();
            int loai_14 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 14).Count();
            int loai_15 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 15).Count();
            int loai_16 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 16).Count();
            int loai_17 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 17).Count();
            int loai_18 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 18).Count();
            int loai_19 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 19).Count();
            int loai_20 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 21).Count();
            int loai_21 = db.SANPHAMs.Where(s => s.LOAI_MALOAI == 20).Count();

            int mh1 = loai_1 + loai_2 + loai_3 + loai_4 + loai_5 + loai_6 + loai_7 + loai_21;
            int mh2 = loai_8 + loai_9 + loai_10 + loai_11 + loai_12;
            int mh3 = loai_13 + loai_14 + loai_15 + loai_16 + loai_17;
            int mh4 = loai_18 + loai_19;
            int mh5 = loai_20;
            SanPhamModel objSP = new SanPhamModel();
            objSP.mh1 = mh1;
            objSP.mh2 = mh2;
            objSP.mh3 = mh3;
            objSP.mh4 = mh4;
            objSP.mh5 = mh5;

            return Json(objSP);
        }
    }
}
