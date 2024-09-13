namespace BusinessObject.DTOs
{
    public class OrderDTO
    {
        public string? Id { get; set; }
        public int? OrderId { get; set; }
        public string? Code { get; set; }
        public int BranchId { get; set; }
        public int CustomerId { get; set; }
        public string Receiver { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string LocationName { get; set; }
        public string? Status { get; set; }
        public bool IsPay { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Note { get; set; }
        public List<OrderDetailDTO>? Details { get; set; }
    }
}
