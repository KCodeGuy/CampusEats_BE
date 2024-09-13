namespace BusinessObject.DTOs
{
    public class PagingDTO<T>
    {
        public int Total { get; set; }
        public int PageSize { get; set; }
        public int CurrentItem { get; set; }
        public List<T> Data { get; set; }
    }
}
