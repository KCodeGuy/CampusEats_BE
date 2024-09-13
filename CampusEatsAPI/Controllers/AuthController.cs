using BusinessObject.DTOs;
using DataAccess.DAOs;
using KiotVietServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusEatsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IKiotVietRepository _kiotVietServices;
        private readonly AccountDAO _accountDAO;

        public AuthController(IKiotVietRepository kiotVietServices, AccountDAO accountDAO)
        {
            _kiotVietServices = kiotVietServices;
            _accountDAO = accountDAO;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                AccountDTO accountDTO = await _accountDAO.LoginAsync(loginDTO.Phone, loginDTO.Password);

                //CustomerDTO response = await _kiotVietServices.GetCustomer(int.Parse(accountDTO.Id));

                return Ok(new APIResponse<CustomerDTO>
                {
                    Code = 200,
                    Success = true,
                    Message = "Login successful!",
                    Data = new CustomerDTO()
                    {
                        Id = long.Parse(accountDTO.Id),
                        Email = accountDTO.Email,
                        Name = accountDTO.Name,
                        Gender = accountDTO.Gender,
                        Address = accountDTO.Address,
                        ContactNumber = accountDTO.Phone
                    }
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<CustomerDTO>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("updatePassword")]
        public async Task<IActionResult> UpdatePassword(LoginDTO loginDTO)
        {
            try
            {
                AccountDTO accountDTO = await _accountDAO.LoginAsync(loginDTO.Phone, loginDTO.Password);

                CustomerDTO response = await _kiotVietServices.GetCustomer(int.Parse(accountDTO.Id));

                return Ok(new APIResponse<CustomerDTO>
                {
                    Code = 200,
                    Success = true,
                    Message = "Login successful!",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<CustomerDTO>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
