using Azure;
using BusinessObject.DTOs;
using KiotVietServices.Entities;
using KiotVietServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhanhVNServices.Entities;
using NhanhVNServices.Repository;

namespace CampusEatsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IKiotVietRepository _kiotVietServices;
        private readonly INhanhVNRepository _nhanhVNRepository;

        public ProductsController(IKiotVietRepository kiotVietServices, INhanhVNRepository nhanhVNRepository)
        {
            _kiotVietServices = kiotVietServices;
            _nhanhVNRepository = nhanhVNRepository;
        }

        [HttpGet("getProducts")]
        public async Task<IActionResult> GetProducts(int pageSize = 100, int currentItem = 1)
        {
            try
            {
                //PagingDTO<ProductDTO> response = await _kiotVietServices.GetProducts(pageSize,currentItem);
                RootProduct rootProduct = await _nhanhVNRepository.GetProducts();

                return Ok(new APIResponse<RootProduct>
                {
                    Code = 200,
                    Success = true,
                    Message = "Get all products successful!",
                    Data = rootProduct
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<PagingDTO<ProductDTO>>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("getProduct")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                //ProductDTO response = await _kiotVietServices.GetProduct(id);

                NhanhVNServices.Entities.ProductResponse product = await _nhanhVNRepository.GetProduct(id);

                ProductDTO productDTO = new ProductDTO()
                {
                    Code = product.IdNhanh + "",
                    Id = long.Parse(product.IdNhanh + ""),
                    FullName = product.Name,
                    Images = new List<string>()
                    {
                        product.Image
                    },
                    Name = product.Name,
                    Price = decimal.Parse(product.Price)
                };

                return Ok(new APIResponse<ProductDTO>
                {
                    Code = 200,
                    Success = true,
                    Message = "Get a product successful!",
                    Data = productDTO
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<ProductDTO>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        
    }
}
