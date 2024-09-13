namespace BusinessObject.DTOs
{
    public class CustomerRequest
    {
        public string? Code { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ContactNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
    }
}
