namespace KiotVietServices.Entities
{
    public class FormulaData
    {
        public long Id { get; set; }
        public long RetailerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public long CategoryId { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Quantity { get; set; }
    }
}
