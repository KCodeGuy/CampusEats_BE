using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanhVNServices.Entities
{
    public class RootOrder
    {
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int Page { get; set; }
        public Dictionary<string, Order> Orders { get; set; }
    }
}
