using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class MenuDTO
    {
        public Guid? Id { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CategogyName { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public List<string>? Images { get; set; }
    }
}
