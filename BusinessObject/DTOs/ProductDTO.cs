namespace BusinessObject.DTOs
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public int RetailerId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ProductType { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string OrderTemplate { get; set; }
        public decimal? Price { get; set; }
        public List<string> Images { get; set; }
    }
}
