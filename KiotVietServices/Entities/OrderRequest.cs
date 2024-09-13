namespace KiotVietServices.Entities
{
    public class OrderRequest
    {
        public int BranchId { get; set; }
        public int CustomerId { get; set; }
        public DeliveryDetail DeliveryDetail { get; set; }
        public List<OrderDetailData> OrderDetails { get; set; }
    }
}
