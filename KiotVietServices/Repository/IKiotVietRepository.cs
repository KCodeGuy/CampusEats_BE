using BusinessObject.DTOs;
using KiotVietServices.Entities;

namespace KiotVietServices.Services
{
    public interface IKiotVietRepository
    {
        public Task<PagingDTO<ProductDTO>> GetProducts(int pageSize = 100, int currentItem = 1);
        public Task<List<CategoryDTO>> GetCategogies();
        public Task<CategoryDTO> GetCategory(int id);
        public Task<ProductDTO> GetProduct(int id);
        public Task<PagingDTO<CustomerDTO>> GetCustomers(int pageSize = 100, int currentItem = 1);
        public Task<CustomerDTO> GetCustomer(int id);
        public Task<CustomerDTO> AddCustomer(CustomerRequest customer);
        public Task<CustomerDTO> UpdateCustomer(CustomerRequest customer, int id);
        public Task<string> DeleteCustomer(int id);
        public Task<OrderDTO> AddOrder(OrderDTO orderDTO);
    }
}
