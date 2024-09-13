using Azure;
using BusinessObject.DTOs;
using DataAccess.DAOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhanhVNServices.Entities;
using NhanhVNServices.Repository;

namespace CampusEatsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuDAO _menuDAO;
        private readonly INhanhVNRepository _nhanhVNRepository;

        public MenuController(MenuDAO menuDAO, INhanhVNRepository nhanhVNRepository)
        {
            _menuDAO = menuDAO;
            _nhanhVNRepository = nhanhVNRepository;
        }

        [HttpGet("getAllMenu")]
        public async Task<IActionResult> GetAllMenu()
        {
            try
            {
                //List<MenuDTO> response = await _menuDAO.GetAllMenuAsync();
                List<MenuDTO> response = new List<MenuDTO>();

                RootProduct rootProduct = await _nhanhVNRepository.GetProducts();

                foreach (var item in rootProduct.Products)
                {
                    response.Add(new MenuDTO
                    {
                        ProductID = int.Parse(item.Value.IdNhanh),
                        FullName = item.Value.Name,
                        Price = int.Parse(item.Value.Price),
                        Id = Guid.NewGuid(),
                        Description = "",
                        Images = new List<string>()
                        {
                            item.Value.Image
                        },
                        Quantity = 1000
                    });
                }

                return Ok(new APIResponse<List<MenuDTO>>
                {
                    Code = 200,
                    Success = true,
                    Message = "Get all menu successful!",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<List<MenuDTO>>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("getMenuToday")]
        public async Task<IActionResult> GetMenuToday()
        {
            try
            {
                //List<MenuDTO> response = await _menuDAO.GetAllMenuTodayAsync();

                List<MenuDTO> response = new List<MenuDTO>();

                RootProduct rootProduct = await _nhanhVNRepository.GetProducts();

                foreach (var item in rootProduct.Products)
                {
                    response.Add(new MenuDTO
                    {
                        ProductID = int.Parse(item.Value.IdNhanh),
                        FullName = item.Value.Name,
                        Price = int.Parse(item.Value.Price),
                        Id = Guid.NewGuid(),
                        Description = "",
                        Images = new List<string>()
                        {
                            item.Value.Image
                        },
                        Quantity = 1000
                    });
                }

                return Ok(new APIResponse<List<MenuDTO>>
                {
                    Code = 200,
                    Success = true,
                    Message = "Get all menu today successful!",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<List<MenuDTO>>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("addMenu")]
        public async Task<IActionResult> AddMenu(List<MenuDTO> menuDTOs)
        {
            try
            {
                int count = await _menuDAO.AddMenuAsync(menuDTOs);

                if(count < 0)
                {
                    return Ok(new APIResponse<List<MenuDTO>>
                    {
                        Code = 500,
                        Success = true,
                        Message = "Add menu fail"
                    });
                }

                return Ok(new APIResponse<int>
                {
                    Code = 200,
                    Success = true,
                    Message = "Add menu successful!"
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<int>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("updateMenu")]
        public async Task<IActionResult> UpdateMenu(MenuDTO menuDTO)
        {
            try
            {
                int count = await _menuDAO.UpdateMenuAsync(menuDTO);

                if (count < 0)
                {
                    return Ok(new APIResponse<int>
                    {
                        Code = 500,
                        Success = true,
                        Message = "Update menu fail"
                    });
                }

                return Ok(new APIResponse<int>
                {
                    Code = 200,
                    Success = true,
                    Message = "Update menu successful!"
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<int>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("deleteMenu")]
        public async Task<IActionResult> DeleteMenu(Guid id)
        {
            try
            {
                int count = await _menuDAO.DeleteMenuAsync(id);

                if (count <= 0)
                {
                    return Ok(new APIResponse<int>
                    {
                        Code = 500,
                        Success = true,
                        Message = "Delete menu fail"
                    });
                }

                return Ok(new APIResponse<int>
                {
                    Code = 200,
                    Success = true,
                    Message = "Delete menu successful!"
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<int>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
