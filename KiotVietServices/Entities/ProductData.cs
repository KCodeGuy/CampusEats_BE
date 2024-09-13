namespace KiotVietServices.Entities
{
    public class ProductData
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public int RetailerId { get; set; }
        public bool AllowsSale { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ProductType { get; set; }
        public bool? IsTopping { get; set; }
        public bool? IsProcessedGoods { get; set; }
        public bool? IsTimeType { get; set; }
        public bool? IsRewardPoint { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string OrderTemplate { get; set; }
        public bool? HasVariants { get; set; }
        public List<AttributeData> Attributes { get; set; }
        public string Unit { get; set; }
        public long MasterUnitId { get; set; }
        public long? MasterProductId { get; set; }
        public int ConversionValue { get; set; }
        public List<UnitData> Units { get; set; }
        public List<string> Images { get; set; }
        public List<InventoryData> Inventories { get; set; }
        public List<PriceBookData> PriceBooks { get; set; }
        public List<ToppingData> Toppings { get; set; }
        public List<FormulaData> Formulas { get; set; }
        public decimal? BasePrice { get; set; }
        public bool? IsTimeServices { get; set; }
        public double? Weight { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
