namespace BusinessObject.DTOs
{
    public class OrderDetailDTO
    {
        public Guid? Id { get; set; }
        public string? OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public string? CategogyName { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public List<string>? Images { get; set; }
    }
}
