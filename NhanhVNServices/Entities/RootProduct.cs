using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanhVNServices.Entities
{
    public class RootProduct
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public Dictionary<int, Product> Products { get; set; }
    }
}
