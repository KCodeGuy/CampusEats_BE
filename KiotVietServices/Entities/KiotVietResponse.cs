namespace KiotVietServices.Entities
{
    public class KiotVietResponse<T>
    {
        public int[] RemoveId { get; set; }
        public int Total { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }
    }
}
