using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NhanhVNServices.Entities
{
    public class ProductDetail
    {
        public int Code { get; set; }
        public Dictionary<string, ProductResponse> Data { get; set; }
    }
}
