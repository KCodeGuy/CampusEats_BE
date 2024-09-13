namespace KiotVietServices.Entities
{
    public class ResponseStatus
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
