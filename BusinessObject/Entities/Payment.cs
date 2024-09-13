using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Entities
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string PaymentContent { get; set; }
        [Required]
        public decimal RequiredAmount { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentMessage { get; set; }
    }
}
