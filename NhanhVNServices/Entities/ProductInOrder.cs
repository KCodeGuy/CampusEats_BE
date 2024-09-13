using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanhVNServices.Entities
{
    public class ProductInOrder
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductBarcode { get; set; }
        public string ProductImage { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string Weight { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public string Imei { get; set; }
        public List<object> GiftProducts { get; set; }
    }
}
