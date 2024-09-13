using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NhanhVNServices.Entities
{
    public class Product
    {
        public string IdNhanh { get; set; }
        public string ParentId { get; set; }
        public string BrandId { get; set; }
        public string BrandName { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public string AvgCost { get; set; }
        public string MerchantCategoryId { get; set; }
        public string MerchantProductId { get; set; }
        public string CategoryId { get; set; }
        public string InternalCategoryId { get; set; }
        public string Code { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string OtherName { get; set; }
        public string ImportPrice { get; set; }
        public string OldPrice { get; set; }
        public string Price { get; set; }
        public string WholesalePrice { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public int ShowHot { get; set; }
        public int ShowNew { get; set; }
        public int ShowHome { get; set; }
        public string Order { get; set; }
        public string PreviewLink { get; set; }
        public string ShippingWeight { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string Height { get; set; }
        public string Vat { get; set; }
        public string CreatedDateTime { get; set; }
        [JsonIgnore]
        public Inventory Inventory { get; set; }
        public string WarrantyAddress { get; set; }
        public string WarrantyPhone { get; set; }
        public string Warranty { get; set; }
        public string CountryName { get; set; }
        public string Unit { get; set; }
    }

}
