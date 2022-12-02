using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QL_CDC.Models
{
    public class SinhVienModel
    {
        [Required(ErrorMessage = "*Chưa nhập tài khoản")]
        [Display(Name = "MSSV: ")]
        [MaxLength(8)]
        public string MSSV { get; set; }

        [Required(ErrorMessage = "*Chưa nhập mật khẩu")]
        [MaxLength(24, ErrorMessage = "*Mật khẩu có độ dài từ 5 đến 24 ký tự")]
        [MinLength(5, ErrorMessage = "*Mật khẩu có độ dài từ 5 đến 24 ký tự")]
        public string MatKhau { get; set; }

        [Compare("MatKhau", ErrorMessage = "Mật khẩu không trùng khớp")]
        [Required(ErrorMessage = "*Chưa nhập mật khẩu")]
        public string XacNhanMatKhau { get; set; }

        [Required(ErrorMessage = "*Chưa nhập họ tên")]
        [MaxLength(200)]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "*Chưa nhập địa chỉ")]
        [MaxLength(500)]
        public string DiaChi { get; set; }
        public string TenHienThi { get; set; }

        [Required(ErrorMessage = "*Chưa nhập số điện thoại")]
        [MaxLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "*Số điện thoại không hợp lệ")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "*Chưa nhập Email")]
        [MaxLength(200)]
        [EmailAddress(ErrorMessage = "*Email không hợp lệ")]
        public string Email { get; set; }
        public DateTime NgayHDCuoi { get; set; }
        public DateTime LanHDCuoi { get; set; }
        public bool TinhTrang { get; set; }
        public string Khoa { get; set; }

        public string ttsv { get; set; }
    }
}
