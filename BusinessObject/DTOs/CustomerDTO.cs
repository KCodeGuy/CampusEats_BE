namespace BusinessObject.DTOs
{
    public class CustomerDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string LocationName { get; set; }
        public string Email { get; set; }
        public string Organization { get; set; }
        public string Comment { get; set; }
        public string TaxCode { get; set; }
        public decimal Debt { get; set; }
        public decimal? TotalInvoiced { get; set; }
        public double? TotalPoint { get; set; }
        public decimal? TotalRevenue { get; set; }
    }
}
