using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanhVNServices.Entities
{
    public class Depot
    {
        public int Remain { get; set; }
        public int Shipping { get; set; }
        public int Damaged { get; set; }
        public int Holding { get; set; }
        public int Warranty { get; set; }
        public int WarrantyHolding { get; set; }
        public int Available { get; set; }
    }
}
