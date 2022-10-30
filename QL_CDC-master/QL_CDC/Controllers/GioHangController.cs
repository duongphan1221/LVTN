using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QL_CDC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QL_CDC.Controllers
{
    public class GioHangController : Controller
    {
        #region
        QL_CDCContext db = new QL_CDCContext();
        public IActionResult HienSanPhamTrongGio()
        {
            var sp = 0;
            if (User.Identity.IsAuthenticated)
            {
                string tk = User.FindFirstValue(ClaimTypes.NameIdentifier);
                sp = db.GIOHANGs.Count(a => a.SV_MSSV == tk);
            }
            return Json(sp);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ThemNhieuSanPhamVaoGio(string mssp, string sl)
        {
            int slsp; int.TryParse(sl, out slsp);
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            GIOHANG find = db.GIOHANGs.Where(a => a.SP_MSSP == mssp && a.SV_MSSV == mssv).FirstOrDefault();
            if(find == null)
            {
                GIOHANG GH = new GIOHANG()
                {
                    SP_MSSP = mssp,
                    SV_MSSV = mssv,
                    GH_SOLUONG = slsp,
                };
                db.GIOHANGs.Add(GH);
                db.SaveChanges();
            }
            else
            {
                db.Entry(find).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                int soluongconlai = (int)db.SANPHAMs.Where(a => a.SP_MSSP == mssp).Select(a => a.SP_CONLAI).FirstOrDefault();
                int soluongdaco = (int)find.GH_SOLUONG;
                if(soluongconlai < soluongdaco + slsp)
                {
                    find.GH_SOLUONG = soluongconlai;
                }
                else
                {
                    find.GH_SOLUONG += slsp;
                }
                db.Entry(find).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            return Json(true);
        }

        [Authorize]
        public IActionResult Them1SanPhamVaoGioHang(string mssp)
        {
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            GIOHANG find = db.GIOHANGs.Where(a => a.SP_MSSP == mssp && a.SV_MSSV == mssv).FirstOrDefault();
            if (find == null)
            {
                GIOHANG GH = new GIOHANG()
                {
                    SP_MSSP = mssp,
                    SV_MSSV = mssv,
                    GH_SOLUONG = 1,
                };
                db.GIOHANGs.Add(GH);
                db.SaveChanges();
            }
            else
            {
                db.Entry(find).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                int soluongconlai = (int)db.SANPHAMs.Where(a => a.SP_MSSP == mssp).Select(a => a.SP_CONLAI).FirstOrDefault();
                int soluongdaco = (int)find.GH_SOLUONG;
                if (soluongconlai < soluongdaco + 1)
                {
                    find.GH_SOLUONG = soluongconlai;
                }
                else
                {
                    find.GH_SOLUONG += 1;
                }
                db.Entry(find).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            return Json(true);
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

        [Authorize]
        public IActionResult GioHang()
        {
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            GioHangModel GH = new GioHangModel();
            GH.TongGia = 0;
            GH.SoLoai = 0;
            GH.SanPham = new List<HangModel>();
            int count = 0;
            foreach(var i in db.GIOHANGs.Where(a => a.SV_MSSV == mssv))
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

            int ship = db.GIOHANGs.Where(a => a.SV_MSSV == mssv).Select(a => a.SP_MSSPNavigation.SV_MSSV).Distinct().Count();

            return View(GH);
        }

        public IActionResult CapNhatGioHang(string sl, string masp)
        {
            int soluong; int.TryParse(sl, out soluong);
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            GIOHANG GH = db.GIOHANGs.Where(a => a.SP_MSSP == masp && a.SV_MSSV == mssv).FirstOrDefault();
            db.Entry(GH).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            GH.GH_SOLUONG = soluong;
            db.Entry(GH).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Json(true);
        }

        public IActionResult XoaKhoiGioHang(string masp)
        {
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            GIOHANG GH = db.GIOHANGs.Where(a => a.SP_MSSP == masp && a.SV_MSSV == mssv).FirstOrDefault();
            db.Remove(GH);
            db.SaveChanges();
            return Json(true);
        }
        #endregion
    }

}
