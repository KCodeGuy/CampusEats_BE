using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NhanhVNServices.Entities
{
    public class ProductResponse
    {
        public int IdNhanh { get; set; }
        public object PrivateId { get; set; }
        public int ParentId { get; set; }
        public object MerchantCategoryId { get; set; }
        public object MerchantProductId { get; set; }
        public object CategoryId { get; set; }
        public object BrandId { get; set; }
        public string BrandName { get; set; }
        public object Code { get; set; }
        public object Barcode { get; set; }
        public string Name { get; set; }
        public object OtherName { get; set; }
        public string ImportPrice { get; set; }
        public string OldPrice { get; set; }
        public string Price { get; set; }
        public string WholesalePrice { get; set; }
        public string Image { get; set; }
        public object Unit { get; set; }
        public List<object> Images { get; set; }
        public string Status { get; set; }
        public string PreviewLink { get; set; }
        public object Advantages { get; set; }
        public object Description { get; set; }
        public object Content { get; set; }
        public int ShowHot { get; set; }
        public int ShowNew { get; set; }
        public int ShowHome { get; set; }
        public object ShippingWeight { get; set; }
        public object Width { get; set; }
        public object Length { get; set; }
        public object Height { get; set; }
        public object Vat { get; set; }
        [JsonIgnore]
        public DateTime CreatedDateTime { get; set; }
        [JsonIgnore]
        public Inventory Inventory { get; set; }
        public object Warranty { get; set; }
        public object WarrantyAddress { get; set; }
        public object WarrantyPhone { get; set; }
        public List<object> Videos { get; set; }
    }
}
