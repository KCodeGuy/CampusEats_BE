namespace KiotVietServices.Entities
{
    public class PriceBookData
    {
        public long PriceBookId { get; set; }
        public string PriceBookName { get; set; }
        public long ProductId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Price { get; set; }
    }
}
