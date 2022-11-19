using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QL_CDC.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QL_CDC.Controllers
{
    public class SanPhamController : Controller
    {
        #region
        QL_CDCContext db = new QL_CDCContext();

        // Trang chu san pham
        public IActionResult Index()
        {
            List<SanPhamModel> SP = new List<SanPhamModel>();
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var x in db.SANPHAMs)
            {
                if(x.SV_MSSV != mssv && x.SP_TINHTRANG == true && x.SP_CONLAI > 0)
                {
                    SanPhamModel s = new SanPhamModel();
                    s.masp = x.SP_MSSP;
                    s.tensp = x.SP_TENSP;
                    s.giagocsp = (double)x.SP_GIA;
                    s.dongiasp = TinhDonGiaSanPham(x.SP_MSSP);
                    s.thoigiansp = (int)x.SP_THOIGIANSUDUNG;
                    s.danhgiasp = LayDanhGiaSanPham(x.SV_MSSV);
                    s.soluongsp = (int)x.SP_CONLAI;
                    s.anhsp = db.HINHANHs.Where(a => a.SP_MSSP == x.SP_MSSP).Select(a => a.HA_LINK).ToList();
                    SP.Add(s);
                }
            }
            return View(SP);
        }

        public IActionResult DoCaNhan()
        {
            List<SanPhamModel> SP = new List<SanPhamModel>();
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var x in db.SANPHAMs)
            {
                if (x.SV_MSSV != mssv && x.SP_TINHTRANG == true && x.SP_CONLAI > 0 && x.LOAI_MALOAI >= 1 && x.LOAI_MALOAI <= 7)
                {
                    SanPhamModel s = new SanPhamModel();
                    s.masp = x.SP_MSSP;
                    s.tensp = x.SP_TENSP;
                    s.giagocsp = (double)x.SP_GIA;
                    s.dongiasp = TinhDonGiaSanPham(x.SP_MSSP);
                    s.thoigiansp = (int)x.SP_THOIGIANSUDUNG;
                    s.danhgiasp = LayDanhGiaSanPham(x.SV_MSSV);
                    s.soluongsp = (int)x.SP_CONLAI;
                    s.anhsp = db.HINHANHs.Where(a => a.SP_MSSP == x.SP_MSSP).Select(a => a.HA_LINK).ToList();
                    SP.Add(s);
                }
            }
            return View(SP);
        }

        public IActionResult DoDienTu()
        {
            List<SanPhamModel> SP = new List<SanPhamModel>();
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var x in db.SANPHAMs)
            {
                if (x.SV_MSSV != mssv && x.SP_TINHTRANG == true && x.SP_CONLAI > 0 && x.LOAI_MALOAI >= 8 && x.LOAI_MALOAI <= 12 || x.LOAI_MALOAI == 14 || x.LOAI_MALOAI == 15 || x.LOAI_MALOAI == 21)
                {
                    SanPhamModel s = new SanPhamModel();
                    s.masp = x.SP_MSSP;
                    s.tensp = x.SP_TENSP;
                    s.giagocsp = (double)x.SP_GIA;
                    s.dongiasp = TinhDonGiaSanPham(x.SP_MSSP);
                    s.thoigiansp = (int)x.SP_THOIGIANSUDUNG;
                    s.danhgiasp = LayDanhGiaSanPham(x.SV_MSSV);
                    s.soluongsp = (int)x.SP_CONLAI;
                    s.anhsp = db.HINHANHs.Where(a => a.SP_MSSP == x.SP_MSSP).Select(a => a.HA_LINK).ToList();
                    SP.Add(s);
                }
            }
            return View(SP);
        }

        public IActionResult DoNoiThat()
        {
            List<SanPhamModel> SP = new List<SanPhamModel>();
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var x in db.SANPHAMs)
            {
                if (x.SV_MSSV != mssv && x.SP_TINHTRANG == true && x.SP_CONLAI > 0 && x.LOAI_MALOAI >= 13 && x.LOAI_MALOAI <= 17)
                {
                    SanPhamModel s = new SanPhamModel();
                    s.masp = x.SP_MSSP;
                    s.tensp = x.SP_TENSP;
                    s.giagocsp = (double)x.SP_GIA;
                    s.dongiasp = TinhDonGiaSanPham(x.SP_MSSP);
                    s.thoigiansp = (int)x.SP_THOIGIANSUDUNG;
                    s.danhgiasp = LayDanhGiaSanPham(x.SV_MSSV);
                    s.soluongsp = (int)x.SP_CONLAI;
                    s.anhsp = db.HINHANHs.Where(a => a.SP_MSSP == x.SP_MSSP).Select(a => a.HA_LINK).ToList();
                    SP.Add(s);
                }
            }
            return View(SP);
        }

        public IActionResult PhuongTien()
        {
            List<SanPhamModel> SP = new List<SanPhamModel>();
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var x in db.SANPHAMs)
            {
                if (x.SV_MSSV != mssv && x.SP_TINHTRANG == true && x.SP_CONLAI > 0 && x.LOAI_MALOAI >= 18 && x.LOAI_MALOAI <= 19)
                {
                    SanPhamModel s = new SanPhamModel();
                    s.masp = x.SP_MSSP;
                    s.tensp = x.SP_TENSP;
                    s.giagocsp = (double)x.SP_GIA;
                    s.dongiasp = TinhDonGiaSanPham(x.SP_MSSP);
                    s.thoigiansp = (int)x.SP_THOIGIANSUDUNG;
                    s.danhgiasp = LayDanhGiaSanPham(x.SV_MSSV);
                    s.soluongsp = (int)x.SP_CONLAI;
                    s.anhsp = db.HINHANHs.Where(a => a.SP_MSSP == x.SP_MSSP).Select(a => a.HA_LINK).ToList();
                    SP.Add(s);
                }
            }
            return View(SP);
        }


        public IActionResult DoHocTap()
        {
            List<SanPhamModel> SP = new List<SanPhamModel>();
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var x in db.SANPHAMs)
            {
                if (x.SV_MSSV != mssv && x.SP_TINHTRANG == true && x.SP_CONLAI > 0 && x.LOAI_MALOAI == 9 || x.LOAI_MALOAI == 11 || x.LOAI_MALOAI == 20)
                {
                    SanPhamModel s = new SanPhamModel();
                    s.masp = x.SP_MSSP;
                    s.tensp = x.SP_TENSP;
                    s.giagocsp = (double)x.SP_GIA;
                    s.dongiasp = TinhDonGiaSanPham(x.SP_MSSP);
                    s.thoigiansp = (int)x.SP_THOIGIANSUDUNG;
                    s.danhgiasp = LayDanhGiaSanPham(x.SV_MSSV);
                    s.soluongsp = (int)x.SP_CONLAI;
                    s.anhsp = db.HINHANHs.Where(a => a.SP_MSSP == x.SP_MSSP).Select(a => a.HA_LINK).ToList();
                    SP.Add(s);
                }
            }
            return View(SP);
        }

        public IActionResult DoKhac()
        {
            List<SanPhamModel> SP = new List<SanPhamModel>();
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var x in db.SANPHAMs)
            {
                if (x.SV_MSSV != mssv && x.SP_TINHTRANG == true && x.SP_CONLAI > 0 && x.LOAI_MALOAI == 20)
                {
                    SanPhamModel s = new SanPhamModel();
                    s.masp = x.SP_MSSP;
                    s.tensp = x.SP_TENSP;
                    s.giagocsp = (double)x.SP_GIA;
                    s.dongiasp = TinhDonGiaSanPham(x.SP_MSSP);
                    s.thoigiansp = (int)x.SP_THOIGIANSUDUNG;
                    s.danhgiasp = LayDanhGiaSanPham(x.SV_MSSV);
                    s.soluongsp = (int)x.SP_CONLAI;
                    s.anhsp = db.HINHANHs.Where(a => a.SP_MSSP == x.SP_MSSP).Select(a => a.HA_LINK).ToList();
                    SP.Add(s);
                }
            }
            return View(SP);
        }


        public double TinhDonGiaSanPham(string mssp)
        {
            double dongia = 0;
            double phantram = 1;
            if(db.KHUYENMAIs.Where(a => a.SP_MSSP == mssp).FirstOrDefault() != null)
            {
                phantram = (double)db.KHUYENMAIs.Where(a => a.SP_MSSP == mssp).Select(a => a.KM_PHANTRAM).FirstOrDefault();
            }
            double giagoc = (double)db.SANPHAMs.Where(a => a.SP_MSSP == mssp).Select(a => a.SP_GIA).FirstOrDefault();
            dongia = giagoc * phantram;
            return dongia;
        }

        public double LayDanhGiaSanPham(string mssv)
        {
            double danhgia = 0;
            List<NHANXETNGUOIBAN> D = db.NHANXETNGUOIBANs.Where(a => a.SV_MSSV_B == mssv).ToList();
            if(D.Count > 0)
            {
                foreach (var x in D)
                {
                    danhgia += (double)x.NX_GIATRI;
                }
                danhgia /= D.Count;
            }
            return danhgia;
        }

        // Them san pham moi
        [Authorize(Roles = "sv")]
        public IActionResult ThemSanPham()
        {
            List<SelectListModel> L = new List<SelectListModel>();
            foreach(var x in db.LOAIMATHANGs)
            {
                SelectListModel s = new SelectListModel();
                s.id = x.MH_MAMH;
                s.name = x.MH_TENMH;
                L.Add(s);
            }
            return View(L);
        }

        //Them SP do noi that
        public IActionResult ThemSPDoNoiThat()
        {
            List<SelectListModel> L = new List<SelectListModel>();
            foreach (var x in db.LOAIMATHANGs)
            {
                SelectListModel s = new SelectListModel();
                s.id = x.MH_MAMH;
                s.name = x.MH_TENMH;
                L.Add(s);
            }
            return View(L);
        }

        // Them SP do dien tu
        public IActionResult ThemSPDoDienTu()
        {
            List<SelectListModel> L = new List<SelectListModel>();
            foreach (var x in db.LOAIMATHANGs)
            {
                SelectListModel s = new SelectListModel();
                s.id = x.MH_MAMH;
                s.name = x.MH_TENMH;
                L.Add(s);
            }
            return View(L);
        }

        // Them SP Xe
        public IActionResult ThemSPXe()
        {
            List<SelectListModel> L = new List<SelectListModel>();
            foreach (var x in db.LOAIMATHANGs)
            {
                SelectListModel s = new SelectListModel();
                s.id = x.MH_MAMH;
                s.name = x.MH_TENMH;
                L.Add(s);
            }
            return View(L);
        }


        [HttpPost]
        public IActionResult CapNhatLoai(string id)
        {
            int n; int.TryParse(id, out n);
            List<SelectListModel> L = new List<SelectListModel>();
            foreach(var i in db.LOAISANPHAMs)
            {
                if(i.MH_MAMH == n)
                {
                    SelectListModel s = new SelectListModel();
                    s.id = i.LOAI_MALOAI;
                    s.name = i.LOAI_TENLOAI;
                    L.Add(s);
                }
            }
            return Json(L);
        }

        [HttpPost]
        public IActionResult DangSanPham(ThemSanPhamModel model)
        {
            // Them san pham
            SANPHAM SP = new SANPHAM() {
                SP_MSSP = Guid.NewGuid().ToString(),
                LOAI_MALOAI = model.maloai,
                SV_MSSV = User.FindFirstValue(ClaimTypes.NameIdentifier),
                SP_TENSP = model.tensp,
                SP_NGAYDANG = DateTime.Today,
                SP_THOIGIANSUDUNG = model.tg,
                SP_GIA = model.gia,
                SP_CONLAI = model.sl,
                SP_DABAN = 0,
                SP_HANGSX = model.nsx,
                SP_MOTA = model.mota,
                SP_TINHTRANG = false,
                SP_LUOTXEM = 0
            };
            db.SANPHAMs.Add(SP);
            db.SaveChanges();

            // Them hinh anh san pham
            foreach(var i in model.img)
            {
                HINHANH HA = new HINHANH()
                {
                    HA_MSHA = Guid.NewGuid().ToString(),
                    SP_MSSP = SP.SP_MSSP,
                    HA_LINK = UploadImage(i),
                };
                db.HINHANHs.Add(HA);
                db.SaveChanges();
            }
            return Json("");
        }

        public string UploadImage(IFormFile img)
        {
            var filename = Guid.NewGuid().ToString() + img.FileName;
            var filepath = Directory.GetCurrentDirectory() + "\\wwwroot\\sanpham\\" + filename;
            using var image = SixLabors.ImageSharp.Image.Load(img.OpenReadStream());
            image.Mutate(x => x.Resize(500, 500));
            image.Save(filepath);
            var report = "\\sanpham\\" + filename;
            return report;
        }
        
        // Xem chi tiet san pham
        [AllowAnonymous]
        public IActionResult ChiTietSanPham(string id)
        {
            SANPHAM s = db.SANPHAMs.Where(a => a.SP_MSSP == id).FirstOrDefault();
            string mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(s.SV_MSSV == mssv)
            {
                return RedirectToAction("Index","SanPham");
            }
            else
            {
                string nguoidang = db.SINHVIENs.Where(a => a.SV_MSSV == s.SV_MSSV).Select(a => a.SV_TENHIENTHI).FirstOrDefault();
                DateTime d = (DateTime)s.SP_NGAYDANG;
                SanPhamModel SP = new SanPhamModel()
                {
                    masp = s.SP_MSSP,
                    tensp = s.SP_TENSP,
                    msnguoidang = s.SV_MSSV,
                    danhgiasp = LayDanhGiaSanPham(mssv),
                    giagocsp = (double)s.SP_GIA,
                    dongiasp = TinhDonGiaSanPham(id),
                    nguoidangsp = nguoidang,
                    ngaydangsp = d.ToString("dd/MM/yyyy"),
                    thoigiansp = (int)s.SP_THOIGIANSUDUNG,
                    soluongsp = (int)s.SP_CONLAI,
                    motasp = s.SP_MOTA,
                    nsx = s.SP_HANGSX,
                    loai = db.LOAISANPHAMs.Where(a => a.LOAI_MALOAI == s.LOAI_MALOAI).Select(a => a.LOAI_TENLOAI).First(),
                };

                List<string> tempstrlist = new List<string>();
                foreach (var i in db.HINHANHs)
                {
                    if (i.SP_MSSP == id)
                    {
                        string name = i.HA_LINK;
                        tempstrlist.Add(name);
                    }
                }
                SP.anhsp = tempstrlist;

                List<NhanXetModel> B = new List<NhanXetModel>();
                foreach (var i in db.NHANXETNGUOIBANs.OrderByDescending(a => a.NX_NGAY))
                {
                    if (i.SV_MSSV_B == s.SV_MSSV)
                    {
                        NhanXetModel b = new NhanXetModel()
                        {
                            mssv_m = i.SV_MSSV_M,
                            noidung = i.NX_NOIDUNG,
                            danhgia = (int)i.NX_GIATRI,
                            img = db.HINHANHs.Where(a => a.SV_MSSV == i.SV_MSSV_M).Select(a => a.HA_LINK).FirstOrDefault(),
                            ngay = ((DateTime)i.NX_NGAY).ToString("dd/MM/yyyy"),
                        };
                        B.Add(b);
                    }
                }
                SP.nhanxetsp = B;

                db.Entry(s).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                s.SP_LUOTXEM += 1;
                db.Entry(s).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();

                return View(SP);
            }
        }

        // Binh luan nguoi ban
        [Authorize]
        public IActionResult NhanXetSanPham(string sao, string bl, string svb, string mssp)
        {
            int s; int.TryParse(sao, out s);
            var m = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var b = svb;
            NHANXETNGUOIBAN N = db.NHANXETNGUOIBANs.Where(a => a.SV_MSSV_B == b && a.SV_MSSV_M == m).FirstOrDefault();
            if(N != null)
            {
                db.NHANXETNGUOIBANs.Remove(N);
                db.SaveChanges();
            }
            NHANXETNGUOIBAN n = new NHANXETNGUOIBAN()
            {
                SV_MSSV_M = User.FindFirstValue(ClaimTypes.NameIdentifier),
                SV_MSSV_B = svb,
                NX_GIATRI = s,
                NX_NOIDUNG = bl,
                NX_NGAY = DateTime.Now,
            };
            db.NHANXETNGUOIBANs.Add(n);
            db.SaveChanges();

            SANPHAM sp = db.SANPHAMs.Where(a => a.SP_MSSP == mssp).FirstOrDefault();
            SanPhamModel model = new SanPhamModel() {
                nguoidangsp = db.SINHVIENs.Where(a => a.SV_MSSV == svb).Select(a => a.SV_TENHIENTHI).FirstOrDefault(),
                msnguoidang = svb,
                masp = mssp,
            }; 
            List<NhanXetModel> B = new List<NhanXetModel>();
            foreach (var i in db.NHANXETNGUOIBANs.OrderByDescending(a => a.NX_NGAY))
            {
                if (i.SV_MSSV_B == sp.SV_MSSV)
                {
                    NhanXetModel nx = new NhanXetModel()
                    {
                        mssv_m = i.SV_MSSV_M,
                        noidung = i.NX_NOIDUNG,
                        danhgia = (int)i.NX_GIATRI,
                        img = db.HINHANHs.Where(a => a.SV_MSSV == i.SV_MSSV_M).Select(a => a.HA_LINK).FirstOrDefault(),
                        ngay = ((DateTime)i.NX_NGAY).ToString("dd/MM/yyyy"),
                    };
                    B.Add(nx);
                }
            }
            model.nhanxetsp = B;
            return PartialView("_NhanXetPartial", model);
        }

        public IActionResult DanhMucSanPham()
        {
            List<DanhMucModel> D = new List<DanhMucModel>();
            foreach(var a in db.LOAIMATHANGs)
            {
                DanhMucModel d = new DanhMucModel()
                {
                    id = a.MH_MAMH,
                    ten = a.MH_TENMH,
                };
                d.danhmuccon = new List<DanhMucCon>();
                foreach(var b in db.LOAISANPHAMs.Where(x => x.MH_MAMH == a.MH_MAMH))
                {
                    DanhMucCon dm = new DanhMucCon()
                    {
                        id = b.LOAI_MALOAI,
                        ten = b.LOAI_TENLOAI,
                    };
                    d.danhmuccon.Add(dm);
                }
                D.Add(d);
            }
            return Json(D);
        }

        [AllowAnonymous]
        public IActionResult TimKiemTheoLoai(string maloai)
        {
            int ml; int.TryParse(maloai, out ml);
            List<SanPhamModel> SP = new List<SanPhamModel>();
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var x in db.SANPHAMs)
            {
                if (x.SV_MSSV != mssv && x.LOAI_MALOAI == ml)
                {
                    SanPhamModel s = new SanPhamModel();
                    s.masp = x.SP_MSSP;
                    s.tensp = x.SP_TENSP;
                    s.giagocsp = (double)x.SP_GIA;
                    s.dongiasp = TinhDonGiaSanPham(x.SP_MSSP);
                    s.thoigiansp = (int)x.SP_THOIGIANSUDUNG;
                    s.danhgiasp = LayDanhGiaSanPham(x.SV_MSSV);
                    s.soluongsp = (int)x.SP_CONLAI;
                    s.anhsp = db.HINHANHs.Where(a => a.SP_MSSP == x.SP_MSSP).Select(a => a.HA_LINK).ToList();
                    SP.Add(s);
                }
            }
            return View(SP);
        }

        public IActionResult TimKiem(string str)
        {
            str = str.ToLower();
            List<SanPhamModel> SP = new List<SanPhamModel>();
            var mssv = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var x in db.SANPHAMs)
            {
                if (x.SV_MSSV != mssv)
                {
                    var loai = db.LOAISANPHAMs.Where(a => a.LOAI_MALOAI == x.LOAI_MALOAI).Select(a => a.LOAI_TENLOAI).FirstOrDefault();
                    var mamh = db.LOAISANPHAMs.Where(a => a.LOAI_MALOAI == x.LOAI_MALOAI).Select(a => a.MH_MAMH).FirstOrDefault();
                    var mh = db.LOAIMATHANGs.Where(a => a.MH_MAMH == mamh).Select(a => a.MH_TENMH).FirstOrDefault();
                    if(x.SP_TENSP.ToLower().Contains(str) ||  
                        x.SP_MOTA.ToLower().Contains(str) ||
                        x.SP_HANGSX.ToLower().Contains(str) ||
                        loai.ToLower().Contains(str) ||
                        mh.ToLower().Contains(str)
                        )
                    {
                        SanPhamModel s = new SanPhamModel();
                        s.masp = x.SP_MSSP;
                        s.tensp = x.SP_TENSP;
                        s.giagocsp = (double)x.SP_GIA;
                        s.dongiasp = TinhDonGiaSanPham(x.SP_MSSP);
                        s.thoigiansp = (int)x.SP_THOIGIANSUDUNG;
                        s.danhgiasp = LayDanhGiaSanPham(x.SV_MSSV);
                        s.soluongsp = (int)x.SP_CONLAI;
                        s.anhsp = db.HINHANHs.Where(a => a.SP_MSSP == x.SP_MSSP).Select(a => a.HA_LINK).ToList();
                        SP.Add(s);
                    }
                }
            }
            return View(SP);
        }
        #endregion 

        public IActionResult ChonLoaiSPThem()
        {

            return View();
        }
    }
}
