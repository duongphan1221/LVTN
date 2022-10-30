using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QL_CDC.Models
{
    public class DangNhapModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập MSSV")]
        [MaxLength(8)]
        public string MSSV { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [MaxLength(24, ErrorMessage = "*Mật khẩu có độ dài từ 5 đến 24 ký tự")]
        [MinLength(5, ErrorMessage = "*Mật khẩu có độ dài từ 5 đến 24 ký tự")]
        public string MatKhau { get; set; }
    }
}
