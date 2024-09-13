using BusinessObject.DTOs;
using DataAccess.DAOs;
using KiotVietServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhanhVNServices.Repository;

namespace CampusEatsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly INhanhVNRepository _nhanhVNRepository;
        private readonly IKiotVietRepository _kiotVietServices;
        private readonly AccountDAO _accountDAO;

        public CustomersController(IKiotVietRepository kiotVietServices, AccountDAO accountDAO, INhanhVNRepository nhanhVNRepository)
        {
            _nhanhVNRepository = nhanhVNRepository;
            _kiotVietServices = kiotVietServices;
            _accountDAO = accountDAO;
        }

        [HttpGet("getCustomers")]
        public async Task<IActionResult> GetCustomers(int pageSize = 100, int currentItem = 1)
        {
            try
            {
                PagingDTO<CustomerDTO> response = await _kiotVietServices.GetCustomers(pageSize, currentItem);
                return Ok(new APIResponse<PagingDTO<CustomerDTO>>
                {
                    Code = 200,
                    Success = true,
                    Message = "Get all customers successful!",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<PagingDTO<CustomerDTO>>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("getCustomer")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                CustomerDTO response = await _kiotVietServices.GetCustomer(id);
                return Ok(new APIResponse<CustomerDTO>
                {
                    Code = 200,
                    Success = true,
                    Message = "Get a customer successful!",
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

        [HttpPost("addCustomer")]
        public async Task<IActionResult> AddCustomer(CustomerRequest customer)
        {
            try
            {
                //CustomerDTO response = await _kiotVietServices.AddCustomer(customer);

                NhanhVNServices.Entities.CustomerRequest customerRequest = new NhanhVNServices.Entities.CustomerRequest()
                {
                    Mobile = customer.ContactNumber,
                    Name = customer.Name
                };

                string customerId = await _nhanhVNRepository.AddCustomer(customerRequest);

                if(customerId != "")
                {
                    AccountDTO accountDTO = new AccountDTO
                    {
                        Id = customerId,
                        Password = customer.Password,
                        Phone = customer.ContactNumber,
                        Address = customer.Address,
                        Email = customer.Email,
                        Gender = customer.Gender,
                        Name = customer.Name
                    };

                    await _accountDAO.AddAccountAsync(accountDTO);
                }

                return Ok(new APIResponse<CustomerDTO>
                {
                    Code = 200,
                    Success = true,
                    Message = "Add a customer successful!",
                    Data = new CustomerDTO()
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

        [HttpPut("updateCustomer")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerRequest customer)
        {
            try
            {
                CustomerDTO response = await _kiotVietServices.UpdateCustomer(customer, id);
                return Ok(new APIResponse<CustomerDTO>
                {
                    Code = 200,
                    Success = true,
                    Message = "Update a customer successful!",
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

        [HttpDelete("deleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                string response = await _kiotVietServices.DeleteCustomer(id);
                return Ok(new APIResponse<string>
                {
                    Code = 200,
                    Success = true,
                    Message = "Delete a customer successful!",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<string>
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
