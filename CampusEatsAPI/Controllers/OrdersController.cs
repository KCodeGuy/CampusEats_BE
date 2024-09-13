using DataAccess.DAOs;
using BusinessObject.DTOs;
using KiotVietServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CampusEatsAPI.Services;
using Microsoft.Extensions.Options;
using VNPAYServices.Config;
using Azure.Core;
using BusinessObject.Entities;
using VNPAYServices.Request;
using KiotVietServices.Entities;
using CampusEatsLibrary.Services;
using NhanhVNServices.Repository;

namespace CampusEatsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IKiotVietRepository _kiotVietRepository;
        private readonly OrderDAO _orderDAO;
        private readonly VnpayConfig _vnpayConfig;
        private readonly ICurrentUserService _currentUserService;
        private readonly INhanhVNRepository _nhanhVNRepository;
        private readonly PaymentDAO _paymentDAO;
        private readonly IBaseService _baseService;
        private readonly MenuDAO _menuDAO;

        public OrdersController(IKiotVietRepository kiotVietServices, OrderDAO orderDAO,
            ICurrentUserService currentUserService, INhanhVNRepository nhanhVNRepository,
            IOptions<VnpayConfig> vnpayConfigOptions, PaymentDAO paymentDAO, IBaseService baseService, MenuDAO menuDAO)
        {
            _kiotVietRepository = kiotVietServices;
            _orderDAO = orderDAO;
            _vnpayConfig = vnpayConfigOptions.Value;
            _currentUserService = currentUserService;
            _nhanhVNRepository = nhanhVNRepository;
            _paymentDAO = paymentDAO;
            _baseService = baseService;
            _menuDAO = menuDAO;
        }

        [HttpGet("getOrders")]
        public async Task<IActionResult> GetOrders(int id)
        {
            try
            {
                List<OrderDTO> orderDTOs = await _orderDAO.GetOrdersByCustomerIDAsync(id);

                return Ok(new APIResponse<List<OrderDTO>>
                {
                    Code = 200,
                    Success = true,
                    Message = "Get list orders successful",
                    Data = orderDTOs
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<OrderDTO>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("getOrderByOrderID")]
        public async Task<IActionResult> GetOrderByOrderID(string id)
        {
            try
            {
                OrderDTO orderDTOs = await _orderDAO.GetOrdersByIDAsync(id);

                return Ok(new APIResponse<OrderDTO>
                {
                    Code = 200,
                    Success = true,
                    Message = "Get list order successful",
                    Data = orderDTOs
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<OrderDTO>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("addOrder")]
        public async Task<IActionResult> AddOrder(OrderDTO orderDTO)
        {
            try
            {
                //bool checkExist = true;

                //foreach (var item in orderDTO.Details)
                //{
                //    if (!(await _menuDAO.IsProductExist(item.ProductId, item.Quantity)))
                //    {
                //        checkExist = false; 
                //        break;
                //    }
                //}

                //if (!checkExist)
                //{
                //    return Ok(new APIResponse<OrderDTO>
                //    {
                //        Code = 500,
                //        Success = false,
                //        Message = "Product quantity is not enough"
                //    });
                //}

                OrderDTO orderDTOResponse = await _orderDAO.AddOrderAsync(orderDTO);

                if(!orderDTO.IsPay)
                {
                    //OrderDTO kiotvietResponse = await _kiotVietRepository.AddOrder(orderDTO);

                    NhanhVNServices.Entities.CreateOrderRequest createOrderRequest = new NhanhVNServices.Entities.CreateOrderRequest()
                    {
                        Id = orderDTOResponse.Id,
                        CustomerMobile = orderDTOResponse.ContactNumber,
                        CustomerName = orderDTOResponse.Receiver,
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

                    await _orderDAO.UpdateKiotVietIDAsync(orderDTOResponse.Id, (int)orderId, orderId + "");

                    orderDTOResponse.Code = orderId + "";
                    orderDTOResponse.OrderId = orderId;

                    string orderURL = "https://7j0pgggk-5274.asse.devtunnels.ms/OrderHistory/" + orderDTOResponse.Id;
                    return Ok(new APIResponse<PaymentLinkDTO>
                    {
                        Code = 200,
                        Success = true,
                        Message = "Add an order successful",
                        Data = new PaymentLinkDTO
                        {
                            PaymentId = "",
                            PaymentUrl = orderURL,
                            Order = orderDTOResponse
                        }
                    });
                }

                decimal requiredAmount = 0;

                foreach (var item in orderDTOResponse.Details)
                {
                    requiredAmount += item.Price * item.Quantity;
                }

                DateTime currentTimeInZone7 = _baseService.GetCurrentDate();

                var version = _vnpayConfig.Version;
                var tmnCode = _vnpayConfig.TmnCode;
                var dateTime = currentTimeInZone7;
                var userID = _currentUserService.IpAddress ?? string.Empty;
                var amount = Convert.ToInt32(requiredAmount);
                var paymentCurrency = "VND";
                var paymentContent = orderDTOResponse.Id ?? string.Empty;
                var returnURL = _vnpayConfig.ReturnUrl;

                var vnpayPayRequest = new VnpayPayRequest(version, tmnCode
                                    , dateTime, userID, amount, paymentCurrency,
                                    "other", paymentContent, returnURL, dateTime.ToString("yyyyMMddHHmmss"));

                Guid paymentID = Guid.NewGuid();

                await _paymentDAO.AddPaymentAsync(new Payment
                {
                    Id = paymentID,
                    PaymentContent = paymentContent,
                    PaymentDate = dateTime,
                    PaymentMessage = "01",
                    PaymentStatus = "Payment pending",
                    RequiredAmount = amount
                });

                var paymentUrl = vnpayPayRequest.GetLink(_vnpayConfig.PaymentUrl, _vnpayConfig.HashSecret);

                return Ok(new APIResponse<PaymentLinkDTO>
                {
                    Code = 200,
                    Success = true,
                    Message = "Add an order successful",
                    Data = new PaymentLinkDTO
                    {
                        PaymentId = paymentID.ToString(),
                        PaymentUrl = paymentUrl,
                        Order = orderDTOResponse
                    }
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<OrderDTO>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
