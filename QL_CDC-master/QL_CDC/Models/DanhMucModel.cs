using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QL_CDC.Models
{
    public class DanhMucCon
    {
        public int id { get; set; }
        public string ten { get; set; }
    }
    public class DanhMucModel
    {
        public int id { get; set; }
        public string ten { get; set; }
        public List<DanhMucCon> danhmuccon {get;set;}
    }
}
