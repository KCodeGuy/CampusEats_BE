using BusinessObject.DTOs;
using KiotVietServices.Entities;
using KiotVietServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusEatsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IKiotVietRepository _kiotVietRepository;

        public CategoriesController(IKiotVietRepository kiotVietServices)
        {
            _kiotVietRepository = kiotVietServices;
        }

        [HttpGet("getCategories")]
        public async Task<IActionResult> GetCategogies()
        {
            try
            {
                List<CategoryDTO> response = await _kiotVietRepository.GetCategogies();
                return Ok(new APIResponse<List<CategoryDTO>>
                {
                    Code = 200,
                    Success = true,
                    Message = "Get all categories successful!",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<List<CategoryDTO>>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("getCategory")]
        public async Task<IActionResult> GetCategogy(int id)
        {
            try
            {
                CategoryDTO response = await _kiotVietRepository.GetCategory(id);
                return Ok(new APIResponse<CategoryDTO>
                {
                    Code = 200,
                    Success = true,
                    Message = "Get a category successful!",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<CategoryDTO>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
