﻿namespace BusinessObject.DTOs
{
    public class PaymentLinkDTO
    {
        public string PaymentId { get; set; } = string.Empty;
        public string PaymentUrl { get; set; } = string.Empty;
        public OrderDTO? Order { get; set; }
    }
}
