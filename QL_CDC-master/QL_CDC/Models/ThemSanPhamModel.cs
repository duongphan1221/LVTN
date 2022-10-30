using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QL_CDC.Models
{
    public class ThemSanPhamModel
    {
        public string masp { get; set; }
        public string tensp { get; set; }
        public List<IFormFile> img { get; set; }
        public int mahang { get; set; }
        public int maloai { get; set; }
        public double gia { get; set; }
        public int tg { get; set; }
        public int sl { get; set; }
        public string nsx { get; set; }
        public string mota { get; set; }
    }
}
