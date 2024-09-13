namespace KiotVietServices.Entities
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int RetailerId { get; set; }
        public int BranchId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int SoldById { get; set; }
        public OrderDetailData[] OrderDetails { get; set; }
        public DeliveryDetail OrderDelivery { get; set; }
        public int Status { get; set; }
        public string StatusValue { get; set; }
        public int ToTalQuantity { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPayment { get; set; }
        public string HistoryNote { get; set; }
        public int DiningOption { get; set; }
        public int UsingCod { get; set; }
    }
}
