using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanhVNServices.Entities
{
    public class CreateOrderRequest
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string Type { get; set; }
        public List<ProductInOrderRequest> ProductList { get; set; }
    }
}
