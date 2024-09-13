using BusinessObject.DTOs;
using BusinessObject.Entities;
using CampusEatsLibrary.Services;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class CartDAO
    {
        private ApplicationDbContext _context;
        private static CartDAO _instance;
        private static readonly object _instanceLock = new object();
        private readonly IBaseService _baseService;

        public static CartDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        throw new InvalidOperationException("CartDAO has not been initialized. Call Initialize method first.");
                    }
                    return _instance;
                }
            }
        }

        public CartDAO(ApplicationDbContext dbContext, IBaseService baseService)
        {
            _context = dbContext;
            _baseService = baseService;
        }

        public static void Initialize(ApplicationDbContext dbContext, IBaseService baseService)
        {
            lock (_instanceLock)
            {
                if (_instance == null)
                {
                    _instance = new CartDAO(dbContext, baseService);
                }
            }
        }

        public async Task<CartDTO> AddCartAsync(CartDTO cartDTO)
        {

            try
            {
                DateTime currentTimeInZone7 = _baseService.GetCurrentDate();

                Guid newID = Guid.NewGuid();

                Cart cart = new Cart
                {
                    Id = newID,
                    CreatedDate = currentTimeInZone7,
                    CustomerID = cartDTO.CustomerID,
                    ProductID = cartDTO.ProductID,
                    Quantity = cartDTO.Quantity
                };

                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync();

                cartDTO.Id = newID;
                cartDTO.CreatedDate = currentTimeInZone7;

                return cartDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateQuantityCartAsync(Guid cartID, int quantity)
        {

            try
            {
                DateTime currentTimeInZone7 = _baseService.GetCurrentDate();

                Cart cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartID);

                if (cart == null)
                {
                    throw new Exception("Cart not found");
                }

                cart.Quantity = quantity;
                cart.CreatedDate = currentTimeInZone7;

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CartDTO>> GetCartsAsync(int customerID)
        {
            try
            {
                DateTime currentTimeInZone7 = _baseService.GetCurrentDate();

                DateTime currentDate = currentTimeInZone7.Date; 

                List<CartDTO> cartDTOs = await _context.Carts
                    .Where(c => c.CustomerID == customerID && c.CreatedDate.Date == currentDate)
                    .Select(c => new CartDTO
                    {
                        Id = c.Id,
                        CustomerID = c.CustomerID,
                        CreatedDate = c.CreatedDate,
                        ProductID = c.ProductID,
                        Quantity = c.Quantity
                    })
                    .ToListAsync();

                var cartsToDelete = _context.Carts.Where(c => c.CustomerID == customerID && c.CreatedDate.Date != currentDate);
                _context.Carts.RemoveRange(cartsToDelete);

                await _context.SaveChangesAsync(); 

                return cartDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
