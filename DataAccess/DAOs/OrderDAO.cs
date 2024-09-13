using DataAccess.Context;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using KiotVietServices.Services;
using Newtonsoft.Json;
using CampusEatsLibrary.Services;

namespace DataAccess.DAOs
{
    public class OrderDAO
    {
        private ApplicationDbContext _context;
        private static OrderDAO _instance;
        private static readonly object _instanceLock = new object();
        private IKiotVietRepository _kiotVietRepository;
        private readonly IBaseService _baseService;

        public static OrderDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        throw new InvalidOperationException("OrderDAO has not been initialized. Call Initialize method first.");
                    }
                    return _instance;
                }
            }
        }

        public OrderDAO(ApplicationDbContext dbContext, IKiotVietRepository kiotVietRepository, IBaseService baseService)
        {
            _context = dbContext;
            _kiotVietRepository = kiotVietRepository;
            _baseService = baseService;
        }

        public static void Initialize(ApplicationDbContext dbContext, IKiotVietRepository kiotVietRepository, IBaseService baseService)
        {
            lock (_instanceLock)
            {
                if (_instance == null)
                {
                    _instance = new OrderDAO(dbContext, kiotVietRepository, baseService);
                }
            }
        }

        public async Task<List<OrderDTO>> GetOrdersByCustomerIDAsync(int customerID)
        {
            try
            {
                List<OrderDTO> orderDTOs = await _context.Orders.Include(o => o.Details)
                    .Where(o => o.CustomerId == customerID)
                    .Select(o => new OrderDTO
                    {
                        Id = o.Id,
                        CustomerId = o.CustomerId,
                        Address = o.Address,
                        BranchId = o.BranchId,
                        Code = o.Code,
                        ContactNumber = o.ContactNumber,
                        LocationName = o.LocationName,
                        OrderId = o.KiotVietOrderId,
                        Receiver = o.Receiver,
                        Status = o.Status,
                        AppointmentDate = o.AppointmentDate,
                        Note = o.Note,
                        Details = o.Details.Select(detail => new OrderDetailDTO 
                        {
                            Id = detail.Id,
                            Note = detail.Note,
                            OrderId = detail.OrderId,
                            Price = detail.Price,
                            ProductId = detail.ProductId,
                            Quantity = detail.Quantity,
                            CategogyName = detail.CategogyName,
                            Description = detail.Description,
                            FullName = detail.FullName,
                            Images = JsonConvert.DeserializeObject<List<string>>(detail.Images)
                        }).ToList()
                    })
                    .ToListAsync();

                return orderDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderDTO> GetOrdersByIDAsync(string orderID)
        {
            try
            {
                Order order = await _context.Orders.Include(o => o.Details)
                    .FirstOrDefaultAsync(o => o.Id.Equals(orderID));

                if (order == null)
                {
                    throw new Exception("Order not found");
                };

                OrderDTO orderDTO = new OrderDTO
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    Address = order.Address,
                    BranchId = order.BranchId,
                    Code = order.Code,
                    ContactNumber = order.ContactNumber,
                    LocationName = order.LocationName,
                    OrderId = order.KiotVietOrderId,
                    Receiver = order.Receiver,
                    Status = order.Status,
                    AppointmentDate = order.AppointmentDate,
                    Note = order.Note,
                    Details = order.Details.Select(detail => new OrderDetailDTO
                    {
                        Id = detail.Id,
                        Note = detail.Note,
                        OrderId = detail.OrderId,
                        Price = detail.Price,
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        CategogyName = detail.CategogyName,
                        Description = detail.Description,
                        FullName = detail.FullName,
                        Images = JsonConvert.DeserializeObject<List<string>>(detail.Images)
                    }).ToList()
                };

                return orderDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderDTO> AddOrderAsync(OrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                throw new ArgumentNullException(nameof(orderDTO));
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                //PagingDTO<ProductDTO> pagingDTO = await _kiotVietRepository.GetProducts();
                //List<ProductDTO> productDTOs = pagingDTO.Data;

                DateTime currentTimeInZone7 = _baseService.GetCurrentDate();

                string lastId = await GetLastOrderID();
                string newId = GenerateNewCode(lastId);

                List<OrderDetail> orderDetails = new List<OrderDetail>();

                foreach (var item in orderDTO.Details)
                {
                    //ProductDTO productDTO = productDTOs.FirstOrDefault(p => p.Id == item.ProductId);

                    Guid odID = Guid.NewGuid();

                    orderDetails.Add(new OrderDetail
                    {
                        Id = odID,
                        Note = item.Note,
                        OrderId = newId,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        CategogyName = "",
                        Description = "",
                        FullName = item.FullName,
                        Images = JsonConvert.SerializeObject(item.Images ?? new List<string>())
                    });

                    item.Id = odID;
                    item.OrderId = newId;
                    item.CategogyName = "";
                    item.Description = "";
                    item.FullName = item.FullName;
                    item.Images = item.Images ?? new List<string>();

                    //Menu menu = _context.Menu.FirstOrDefault(m => m.CreatedDate.Date == currentTimeInZone7.Date && m.ProductID == item.ProductId);

                    //if(menu == null || menu.Quantity < item.Quantity)
                    //{
                    //    throw new Exception("Product quantity is not enough");
                    //}

                    //menu.Quantity -= item.Quantity;
                    //await _context.SaveChangesAsync();
                }

                Order order = new Order
                {
                    Id = newId,
                    KiotVietOrderId = orderDTO.OrderId,
                    Code = orderDTO.Code ?? "Null",
                    Address = orderDTO.Address,
                    BranchId = orderDTO.BranchId,
                    ContactNumber = orderDTO.ContactNumber,
                    CustomerId = orderDTO.CustomerId,
                    LocationName = orderDTO.LocationName,
                    Receiver = orderDTO.Receiver,
                    Status = OrderStatus.PENDING.ToString(),
                    CreateDate = currentTimeInZone7,
                    Note = orderDTO.Note ?? "",
                    AppointmentDate = orderDTO.AppointmentDate ?? currentTimeInZone7.AddMinutes(15),
            };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                await _context.OrderDetails.AddRangeAsync(orderDetails);
                await _context.SaveChangesAsync();

                orderDTO.Status = OrderStatus.PENDING.ToString();
                orderDTO.Id = newId;
                orderDTO.AppointmentDate = orderDTO.AppointmentDate ?? currentTimeInZone7.AddMinutes(15);

                await transaction.CommitAsync();
                return orderDTO;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateStatusOrderAsync(string id, OrderStatus status)
        {

            try
            {
                Order order = await _context.Orders.FirstOrDefaultAsync(o => o.Id.Equals(id));

                if(order == null)
                {
                    throw new Exception("Order not found");
                }

                order.Status = status.ToString();

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateKiotVietIDAsync(string id, int? kiotvietID = null, string code = null)
        {

            try
            {
                Order order = await _context.Orders.FirstOrDefaultAsync(o => o.Id.Equals(id));

                if (order == null)
                {
                    throw new Exception("Order not found");
                }

                if(kiotvietID != null)
                {
                    order.KiotVietOrderId = kiotvietID;
                }
                
                if(code != null)
                {
                    order.Code = code;
                }

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<string> GetLastOrderID()
        {
            try
            {
                Order lastOrder = await _context.Orders.OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                if (lastOrder == null)
                {
                    string currentDatePart = DateTime.Now.ToString("yyyyMMdd");
                    return $"OD{currentDatePart}0000";
                }

                return lastOrder.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GenerateNewCode(string oldCode)
        {
            if (oldCode.Length != 14 || !oldCode.StartsWith("OD"))
            {
                throw new ArgumentException("Invalid input code format");
            }

            string oldDatePart = oldCode.Substring(2, 8);

            DateTime currentTimeInZone7 = _baseService.GetCurrentDate();

            string currentDatePart = currentTimeInZone7.ToString("yyyyMMdd");

            if (oldDatePart == currentDatePart)
            {
                string oldNumberPart = oldCode.Substring(10, 4);
                if (int.TryParse(oldNumberPart, out int currentNumber))
                {
                    int newNumber = currentNumber + 1;
                    string newNumberPart = newNumber.ToString("0000");
                    return $"OD{currentDatePart}{newNumberPart}";
                }
                else
                {
                    throw new ArgumentException("Invalid input code format");
                }
            }
            else
            {
                return $"OD{currentDatePart}0001";
            }
        }
    }
}
