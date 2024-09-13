using DataAccess.DAOs;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using CampusEatsAPI.Services;
using VNPAYServices.Config;
using VNPAYServices.Request;
using VNPAYServices.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using CampusEatsLibrary.Services;
using KiotVietServices.Services;
using NhanhVNServices.Repository;

namespace CampusEatsAPI.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly VnpayConfig _vnpayConfig;
        private readonly ICurrentUserService _currentUserService;
        private readonly INhanhVNRepository _nhanhVNRepository;
        private readonly PaymentDAO _paymentDAO;
        private readonly OrderDAO _orderDAO;
        private readonly IBaseService _baseService;
        private readonly IKiotVietRepository _kiotVietRepository;

        public PaymentsController(ICurrentUserService currentUserService, INhanhVNRepository nhanhVNRepository,
            IOptions<VnpayConfig> vnpayConfigOptions, PaymentDAO paymentDAO, OrderDAO orderDAO, IBaseService baseService,
            IKiotVietRepository kiotVietRepository)
        {
            _vnpayConfig = vnpayConfigOptions.Value;
            _currentUserService = currentUserService;
            _nhanhVNRepository = nhanhVNRepository;
            _paymentDAO = paymentDAO;
            _orderDAO = orderDAO;
            _baseService = baseService;
            _kiotVietRepository = kiotVietRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDTO request)
        {
            DateTime currentTimeInZone7 = _baseService.GetCurrentDate();

            var version = _vnpayConfig.Version;
            var tmnCode = _vnpayConfig.TmnCode;
            var dateTime = currentTimeInZone7;
            var userID = _currentUserService.IpAddress ?? string.Empty;
            var amount = request.RequiredAmount ?? 0;
            var paymentCurrency = request.PaymentCurrency ?? string.Empty;
            var paymentContent = request.PaymentContent ?? string.Empty;
            var returnURL = _vnpayConfig.ReturnUrl;

            var vnpayPayRequest = new VnpayPayRequest(version, tmnCode
                                , dateTime , userID , amount , paymentCurrency ,
                                "other", paymentContent , returnURL, dateTime.ToString("yyyyMMddHHmmss"));

            Guid paymentID = Guid.NewGuid();

            await _paymentDAO.AddPaymentAsync(new Payment
            {
                Id = paymentID,
                PaymentContent = paymentContent,
                PaymentDate = dateTime,
                PaymentMessage = "",
                PaymentStatus = "",
                RequiredAmount = amount
            });

            var paymentUrl = vnpayPayRequest.GetLink(_vnpayConfig.PaymentUrl, _vnpayConfig.HashSecret);

            return Ok(new APIResponse<PaymentLinkDTO>
            {
                Code = 200,
                Success = true,
                Message = "Get link payment successful",
                Data = new PaymentLinkDTO
                {
                    PaymentId = paymentID.ToString(),
                    PaymentUrl = paymentUrl
                }
            });
        }

        [HttpGet]
        [Route("vnpay-return")]
        public async Task<IActionResult> VnpayReturn([FromQuery] VnpayPayResponse response)
        {
            string returnUrl = _vnpayConfig.ReturnUrl;

            bool isValidSignature = response.IsValidSignature(_vnpayConfig.HashSecret);

            var resultData = new PaymentReturnDTO();

            if (isValidSignature)
            {

                resultData.PaymentContent = response.vnp_OrderInfo;
                resultData.Amount = response.vnp_Amount;
                resultData.PaymentDate = response.vnp_PayDate;

                if (response.vnp_ResponseCode == "00")
                {
                    resultData.PaymentStatus = "00";
                    resultData.PaymentMessage = "Payment process successful";

                    await _orderDAO.UpdateStatusOrderAsync(resultData.PaymentContent, OrderStatus.PAID);

                    OrderDTO orderDTO = await _orderDAO.GetOrdersByIDAsync(resultData.PaymentContent);

                    NhanhVNServices.Entities.CreateOrderRequest createOrderRequest = new NhanhVNServices.Entities.CreateOrderRequest()
                    {
                        Id = orderDTO.Id,
                        CustomerMobile = orderDTO.ContactNumber,
                        CustomerName = orderDTO.Receiver,
                        Type = "Shopping",
                        ProductList = orderDTO.Details.Select(item => new NhanhVNServices.Entities.ProductInOrderRequest()
                        {
                            Id = item.ProductId + "",
                            IdNhanh = item.ProductId,
                            Name = item.FullName,
                            Price = (int)Math.Floor(item.Price),
                            Quantity = item.Quantity
                        }).ToList()
                    };

                    int orderId = await _nhanhVNRepository.AddOrder(createOrderRequest);

                    //OrderDTO kiotvietResponse = await _kiotVietRepository.AddOrder(orderDTO);

                    await _orderDAO.UpdateKiotVietIDAsync(resultData.PaymentContent, (int)orderId, orderId + "");
                }
                else
                {
                    resultData.PaymentStatus = "10";
                    resultData.PaymentMessage = "Payment process failed";
                }

                await _paymentDAO.UpdatePaymentStatusAsync(resultData.PaymentContent, resultData.PaymentMessage, resultData.PaymentStatus);
            }
            else
            {
                resultData.PaymentStatus = "99";
                resultData.PaymentMessage = "Invalid signature in response";
            }

            if (returnUrl.EndsWith("/"))
                returnUrl = returnUrl.Remove(returnUrl.Length - 1, 1);
            return Redirect($"https://campuseats-three.vercel.app/OrderHistory/{resultData.PaymentContent}");
        }


    }
}
