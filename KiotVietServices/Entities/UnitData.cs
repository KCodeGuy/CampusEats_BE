namespace KiotVietServices.Entities
{
    public class UnitData
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Unit { get; set; }
        public double ConversionValue { get; set; }
        public decimal BasePrice { get; set; }
    }
}
