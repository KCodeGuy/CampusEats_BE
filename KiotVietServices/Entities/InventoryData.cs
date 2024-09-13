namespace KiotVietServices.Entities
{
    public class InventoryData
    {
        public long ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public double? OnHand { get; set; }
        public decimal? Cost { get; set; }
        public double Reserved { get; set; }
    }
}
