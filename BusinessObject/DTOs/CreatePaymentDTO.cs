namespace BusinessObject.DTOs
{
    public class CreatePaymentDTO
    {
        public string PaymentContent { get; set; } = string.Empty;
        public string PaymentCurrency { get; set; } = "VND";
        public decimal? RequiredAmount { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.Now;
        public DateTime? ExpireDate { get; set; } = DateTime.Now.AddMinutes(15);
        public string? PaymentLanguage { get; set; } = "vn";
        //public string? PaymentDestinationId { get; set; } = string.Empty;
    }
}
