using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BusinessObject.Entities
{
    public class Order
    {
        [Key]
        public string Id { get; set; }
        public int? KiotVietOrderId { get; set; }
        public string Code { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string Receiver {  get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string LocationName { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        public string Note { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
    }
}
