using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanhVNServices.Entities
{
    public class ProductInOrderRequest
    {
        public string Id { get; set; }
        public int IdNhanh { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
