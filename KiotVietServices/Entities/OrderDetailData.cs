namespace KiotVietServices.Entities
{
    public class OrderDetailData
    {
        public Guid? Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
    }
}
