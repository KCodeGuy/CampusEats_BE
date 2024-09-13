using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class CartDTO
    {
        public Guid? Id { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
