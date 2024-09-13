using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanhVNServices.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string PrivateId { get; set; }
        public string ShopOrderId { get; set; }
        public int Channel { get; set; }
        public int SaleChannel { get; set; }
        public string MerchantTrackingNumber { get; set; }
        public int DepotId { get; set; }
        public string HandoverId { get; set; }
        public string DepotName { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public int MoneyDiscount { get; set; }
        public int MoneyDeposit { get; set; }
        public int MoneyTransfer { get; set; }
        public string DepositAccount { get; set; }
        public string TransferAccount { get; set; }
        public int UsedPoints { get; set; }
        public int MoneyUsedPoints { get; set; }
        public int CarrierId { get; set; }
        public string CarrierName { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string CarrierCode { get; set; }
        public int ShipFee { get; set; }
        public int CodFee { get; set; }
        public int CustomerShipFee { get; set; }
        public int ReturnFee { get; set; }
        public int OverWeightShipFee { get; set; }
        public int DeclaredFee { get; set; }
        public string Description { get; set; }
        public string PrivateDescription { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerAddress { get; set; }
        public int ShipToCityLocationId { get; set; }
        public string ShipToDistrictLocationId { get; set; }
        public int CustomerCityId { get; set; }
        public string CustomerCity { get; set; }
        public int CustomerDistrictId { get; set; }
        public string CustomerDistrict { get; set; }
        public int ShipToWardLocationId { get; set; }
        public string CustomerWard { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByName { get; set; }
        public string SaleId { get; set; }
        public string SaleName { get; set; }
        public string CreatedDateTime { get; set; }
        public string DeliveryDate { get; set; }
        public string SendCarrierDate { get; set; }
        public string StatusName { get; set; }
        public string StatusCode { get; set; }
        public int CalcTotalMoney { get; set; }
        public int TrafficSourceId { get; set; }
        public string TrafficSourceName { get; set; }
        public string AffiliateCode { get; set; }
        public int AffiliateBonusCash { get; set; }
        public int AffiliateBonusPercent { get; set; }
        public List<ProductInOrder> Products { get; set; }
        public List<object> Tags { get; set; }
        public string CouponCode { get; set; }
        public string ReturnFromOrderId { get; set; }
        public string UtmSource { get; set; }
        public string UtmMedium { get; set; }
        public string UtmCampaign { get; set; }
        public Facebook Facebook { get; set; }
    }
}
