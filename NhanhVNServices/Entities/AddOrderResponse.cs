using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanhVNServices.Entities
{
    public class AddOrderResponse
    {
        public int OrderId { get; set; }
        public int ShipFee { get; set; }
        public int CodFee { get; set; }
    }
}
