using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QL_CDC.Models
{
    public class TinNhan
    {
        public string noidung { get; set;}
        public string thoigian { get; set; }
        public DateTime thoigian_dt { get; set; }
        public Boolean dadoc { get; set; }
        public Boolean latinguidi { get; set; }
    }
    public class ChatModel
    {
        public List<TinNhan> TatCaTinNhan { get; set; }
        public string MaNguoiGui { get; set; }
        public string MaNguoiNhan { get; set; }
        public string TenShop { get; set; }
        public TinNhan TinCuoi { get; set; }
        public string img { get; set; }
    }
}
