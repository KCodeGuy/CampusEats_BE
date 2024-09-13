using BusinessObject.DTOs;
using BusinessObject.Entities;
using CampusEatsLibrary.Services;
using DataAccess.Context;
using KiotVietServices.Entities;
using KiotVietServices.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class MenuDAO
    {
        private ApplicationDbContext _context;
        private static MenuDAO _instance;
        private static readonly object _instanceLock = new object();
        private readonly IBaseService _baseService;
        private readonly IKiotVietRepository _kiotVietRepository;

        public static MenuDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        throw new InvalidOperationException("MenuDAO has not been initialized. Call Initialize method first.");
                    }
                    return _instance;
                }
            }
        }

        public MenuDAO(ApplicationDbContext dbContext, IBaseService baseService, IKiotVietRepository kiotVietRepository)
        {
            _context = dbContext;
            _baseService = baseService;
            _kiotVietRepository = kiotVietRepository;
        }

        public static void Initialize(ApplicationDbContext dbContext, IBaseService baseService, IKiotVietRepository kiotVietRepository)
        {
            lock (_instanceLock)
            {
                if (_instance == null)
                {
                    _instance = new MenuDAO(dbContext, baseService, kiotVietRepository);
                }
            }
        }

        public async Task<List<MenuDTO>> GetAllMenuAsync()
        {
            try
            {
                PagingDTO<ProductDTO> pagingDTO = await _kiotVietRepository.GetProducts();
                List<ProductDTO> productDTOs = pagingDTO.Data;

                List<Menu> menu = await _context.Menu.ToListAsync();

                List<MenuDTO> list = new List<MenuDTO>();

                foreach (var item in menu)
                {
                    ProductDTO productDTO = productDTOs.FirstOrDefault(p => p.Id == item.ProductID);

                    list.Add(new MenuDTO
                    {
                        Id = item.Id,
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        Price = productDTO?.Price,
                        CreatedDate = item.CreatedDate,
                        CategogyName = productDTO?.CategoryName ?? "",
                        Description = productDTO?.Description ?? "",
                        FullName = productDTO?.FullName ?? "",
                        Images = productDTO?.Images ?? new List<string>()
                    });
                };

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MenuDTO>> GetAllMenuTodayAsync()
        {
            try
            {
                PagingDTO<ProductDTO> pagingDTO = await _kiotVietRepository.GetProducts();
                List<ProductDTO> productDTOs = pagingDTO.Data;
                DateTime currentTimeInZone7 = _baseService.GetCurrentDate();

                List<Menu> menu = await _context.Menu.Where(m => m.CreatedDate.Date == currentTimeInZone7.Date).ToListAsync();

                List<MenuDTO> list = new List<MenuDTO>();

                foreach (var item in menu)
                {
                    ProductDTO productDTO = productDTOs.FirstOrDefault(p => p.Id == item.ProductID);

                    list.Add(new MenuDTO
                    {
                        Id = item.Id,
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        CreatedDate = item.CreatedDate,
                        Price = productDTO?.Price,
                        CategogyName = productDTO?.CategoryName ?? "",
                        Description = productDTO?.Description ?? "",
                        FullName = productDTO?.FullName ?? "",
                        Images = productDTO?.Images ?? new List<string>()
                    });
                };

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddMenuAsync(List<MenuDTO> menuDTOs)
        {
            try
            {
                bool checkQuantity = menuDTOs.Any(m => m.Quantity < 0);

                if(checkQuantity)
                {
                    throw new Exception("Quantity must be greater than zero");
                }

                PagingDTO<ProductDTO> products = await _kiotVietRepository.GetProducts();
                List<ProductDTO> productDTOs = products.Data;

                DateTime currentTimeInZone7 = _baseService.GetCurrentDate();

                int count = 0;

                foreach (var menuDTO in menuDTOs)
                {
                    Menu existingMenu = await _context.Menu
                        .FirstOrDefaultAsync(m => m.ProductID == menuDTO.ProductID && m.CreatedDate.Date == currentTimeInZone7.Date);

                    if (existingMenu != null)
                    {
                        existingMenu.Quantity += menuDTO.Quantity;
                        count += await _context.SaveChangesAsync();
                    }
                    else
                    {
                        Menu newMenu = new Menu
                        {
                            Id = Guid.NewGuid(),
                            CreatedDate = currentTimeInZone7,
                            ProductID = menuDTO.ProductID,
                            Quantity = menuDTO.Quantity
                        };

                        _context.Menu.Add(newMenu);
                        count += await _context.SaveChangesAsync();
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<int> UpdateMenuAsync(MenuDTO menuDTO)
        {
            if(menuDTO.Id == null)
            {
                throw new ArgumentNullException(nameof(menuDTO));
            }

            if (menuDTO.Quantity < 0)
            {
                throw new Exception("Quantity must be greater than zero");
            }

            try
            {
                DateTime currentTimeInZone7 = _baseService.GetCurrentDate();
                PagingDTO<ProductDTO> products = await _kiotVietRepository.GetProducts();
                List<ProductDTO> productDTOs = products.Data;

                bool checkID = !productDTOs.Any(itemA => itemA.Id == menuDTO.ProductID);

                if (checkID)
                {
                    throw new Exception("ProductID not exist");
                }

                Menu menu = await _context.Menu.FirstOrDefaultAsync(m => m.Id == menuDTO.Id);

                if(menu == null)
                {
                    throw new Exception("Menu not found");
                }

                Menu existingMenu = await _context.Menu
                        .FirstOrDefaultAsync(m => m.ProductID == menuDTO.ProductID && m.CreatedDate.Date == currentTimeInZone7.Date);

                if (existingMenu != null && existingMenu.Id != menu.Id)
                {
                    existingMenu.Quantity = menuDTO.Quantity;
                    _context.Menu.Remove(menu);
                }
                else
                {
                    menu.Quantity = menuDTO.Quantity;
                    menu.ProductID = menuDTO.ProductID;
                }

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteMenuAsync(Guid menuID)
        {
            if (menuID == null)
            {
                throw new ArgumentNullException(nameof(menuID));
            }

            try
            {
                Menu menu = await _context.Menu.FirstOrDefaultAsync(m => m.Id == menuID);

                if (menu == null)
                {
                    throw new Exception("Menu not found");
                }

                _context.Menu.Remove(menu);

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsProductExist(int productID, int quantity)
        {
            try
            {
                DateTime currentTimeInZone7 = _baseService.GetCurrentDate();

                Menu menu = await _context.Menu.FirstOrDefaultAsync(m => m.ProductID == productID && m.CreatedDate.Date == currentTimeInZone7.Date);

                if(menu == null)
                {
                    return false;
                }

                if(menu.Quantity < quantity)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
