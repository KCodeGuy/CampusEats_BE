namespace KiotVietServices.Entities
{
    public class CategoryResponse
    {
        public int Total { get; set; }
        public int PageSize { get; set; }
        public List<CategoryData> Data { get; set; }
        public int[] RemovedIds { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
