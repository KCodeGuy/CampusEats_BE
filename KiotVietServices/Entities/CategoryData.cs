namespace KiotVietServices.Entities
{
    public class CategoryData
    {
        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        public string CategoryName { get; set; }
        public int RetailerId { get; set; }
        public bool? HasChild { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public CategoryData? Children { get; set; }
    }
}
