using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Entities
{
    public class OrderDetail
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public string CategogyName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Images { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
    }
}
