using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QL_CDC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QL_CDC.Controllers
{
    public class TaiKhoanController : Controller
    {
        QL_CDCContext db = new QL_CDCContext();

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        [ActionName("DangNhap")]
        public IActionResult TaskDangNhap(DangNhapModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                SINHVIEN sv = db.SINHVIENs.Where(x => x.SV_MSSV == model.MSSV).FirstOrDefault();
                if (sv == null)
                {
                    ModelState.AddModelError(nameof(DangNhapModel.MSSV), "Chưa nhập tài khoản!");
                    return View(model);
                }
                else if (sv.SV_TINHTRANG == false)
                {
                    ModelState.AddModelError(nameof(DangNhapModel.MSSV), "Tài khoản đã bị khóa!");
                    return View(model);
                }
                else
                {
                    var mk = MaHoaMatKhau(model.MatKhau);
                    if (mk != sv.SV_MATKHAU)
                    {
                        ModelState.AddModelError(nameof(DangNhapModel.MSSV), "Chưa nhập mật khẩu!");
                        return View(model);
                    }
                    else
                    {
                        string role = "";
                        if (sv.SV_ADMIN == true)
                        {
                            role = "ad";                           
                            var claims = new[] {
                    new Claim(ClaimTypes.Name, sv.SV_TENHIENTHI),
                    new Claim(ClaimTypes.NameIdentifier, sv.SV_MSSV),
                    new Claim(ClaimTypes.Role, role)};
                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(identity));

                            db.Entry(sv).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                            sv.SV_LANHDCUOI = DateTime.Today;
                            db.Entry(sv).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            db.SaveChanges();

                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            role = "sv";
                            var claims = new[] {
                    new Claim(ClaimTypes.Name, sv.SV_TENHIENTHI),
                    new Claim(ClaimTypes.NameIdentifier, sv.SV_MSSV),
                    new Claim(ClaimTypes.Role, role)};
                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(identity));

                            db.Entry(sv).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                            sv.SV_LANHDCUOI = DateTime.Today;
                            db.Entry(sv).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            db.SaveChanges();

                            return RedirectToAction("Index", "SanPham");
                        }
                        
                    }
                }
            }
        }

        public string MaHoaMatKhau(string mk)
        {
            string mkmh = "";
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(mk));
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            mkmh = sb.ToString();
            return mkmh;
        }

        [Authorize]
        public async Task<IActionResult> DangXuat()
        {  
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","SanPham");
        }

        public IActionResult LayIDUser()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Json(id);
        }

        public IActionResult Test()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        public IActionResult KiemTraTonTaiMSSV(string mssv)
        {
            SINHVIEN S = db.SINHVIENs.Where(a => a.SV_MSSV == mssv).FirstOrDefault();
            if(S == null)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }

        [HttpPost]
        [ActionName("DangKy")]
        [ValidateAntiForgeryToken]
        public IActionResult TaskDangKy(SinhVienModel model)
        {
            SINHVIEN sv = db.SINHVIENs.Where(a => a.SV_MSSV == model.MSSV).FirstOrDefault();
            if(sv != null)
            {
                ModelState.AddModelError(nameof(SinhVienModel.MSSV), "*MSSV này đã được đăng ký");
                return View(model);
            }
            else
            {
                sv = new SINHVIEN()
                {
                    SV_MSSV = model.MSSV,
                    SV_MATKHAU = MaHoaMatKhau(model.MatKhau),
                    SV_HOTEN = model.HoTen,
                    SV_TENHIENTHI = model.HoTen,
                    SV_NGAYTAOTK = DateTime.Today,
                    SV_LANHDCUOI = DateTime.Today,
                    SV_DIACHIGIAOHANG = model.DiaChi,
                    SV_SDT = model.SDT,
                    SV_EMAIL = model.Email,
                    SV_TINHTRANG = true,
                    SV_ADMIN = false,
                };
                db.SINHVIENs.Add(sv);
                db.SaveChanges();
                return RedirectToAction("DangNhap");
            }
        }

        [Authorize]
        public IActionResult ThongTinTaiKhoan()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            SINHVIEN SV = db.SINHVIENs.Where(a => a.SV_MSSV == id).FirstOrDefault();
            SinhVienModel model = new SinhVienModel()
            {
                MSSV = SV.SV_MSSV,
                HoTen = SV.SV_HOTEN,
                TenHienThi = SV.SV_TENHIENTHI,
                DiaChi = SV.SV_DIACHIGIAOHANG,
                SDT = SV.SV_SDT,
                Email = SV.SV_EMAIL
            };
            return View(model);
        }

        public IActionResult SuaThongTin(string tenhienthi, string email, string sdt, string hoten, string dc)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            SINHVIEN sv = db.SINHVIENs.Where(a => a.SV_MSSV == id).FirstOrDefault();
            db.Entry(sv).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            sv.SV_TENHIENTHI = tenhienthi;
            sv.SV_EMAIL = email;
            sv.SV_SDT = sdt;
            sv.SV_HOTEN = hoten;
            sv.SV_DIACHIGIAOHANG = dc;
            db.Entry(sv).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Json("");
        }

        public IActionResult DoiMatKhau()
        {
            //var sv = db.SINHVIENs.Where(s => s.SV_MSSV == a.MSSV).FirstOrDefault();
            //if (a.MatKhau != sv.SV_MATKHAU)
            //{

            //}

            return View();
        }

        public IActionResult DoiMK(string tenhienthi, string email, string sdt, string hoten, string dc)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            SINHVIEN sv = db.SINHVIENs.Where(a => a.SV_MSSV == id).FirstOrDefault();
            db.Entry(sv).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            sv.SV_TENHIENTHI = tenhienthi;
            sv.SV_EMAIL = email;
            sv.SV_SDT = sdt;
            sv.SV_HOTEN = hoten;
            sv.SV_DIACHIGIAOHANG = dc;
            db.Entry(sv).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Json("");
        }
    }
}
