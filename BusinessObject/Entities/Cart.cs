using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
