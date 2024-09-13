using BusinessObject.DTOs;
using KiotVietServices.Entities;
using Newtonsoft.Json;

namespace KiotVietServices.Services
{
    public class KiotVietRepository : IKiotVietRepository
    {
        private readonly ApiClient _apiClient;

        public KiotVietRepository(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<PagingDTO<ProductDTO>> GetProducts(int pageSize = 100, int currentItem = 1)
        {
            try
            {
                KiotVietResponse<ProductData> response = await _apiClient.GetAsync<KiotVietResponse<ProductData>>($"/products?pageSize={pageSize}&currentItem={currentItem}");

                List<ProductDTO> products = response.Data
                    .Select(item => new ProductDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        Code = item.Code,
                        Description = item.Description,
                        FullName = item.FullName,
                        Images = item.Images,
                        OrderTemplate = item.OrderTemplate,
                        ProductType = item.ProductType,
                        RetailerId = item.RetailerId,
                        Price = item.BasePrice
                    }).ToList();

                PagingDTO<ProductDTO> pagingDTO = new PagingDTO<ProductDTO>
                {
                    Total = response.Total,
                    PageSize = response.PageSize,
                    CurrentItem = currentItem,
                    Data = products
                };

                return pagingDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductDTO> GetProduct(int id)
        {
            try
            {
                ProductData response = await _apiClient.GetAsync<ProductData>($"/products/{id}");

                ProductDTO productDTO = new ProductDTO
                {
                    Id = response.Id,
                    Name = response.Name,
                    CategoryId = response.CategoryId,
                    CategoryName = response.CategoryName,
                    Code = response.Code,
                    Description = response.Description,
                    FullName = response.FullName,
                    Images = response.Images,
                    OrderTemplate = response.OrderTemplate,
                    ProductType = response.ProductType,
                    RetailerId = response.RetailerId,
                    Price = response.BasePrice
                };

                return productDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CategoryDTO>> GetCategogies()
        {
            try
            {
                CategoryResponse response = await _apiClient.GetAsync<CategoryResponse>("/categories");

                List<CategoryDTO> categories = response.Data
                    .Select(item => new CategoryDTO
                    {
                        Id = item.CategoryId,
                        Name = item.CategoryName
                    }).ToList();

                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CategoryDTO> GetCategory(int id)
        {
            try
            {
                CategoryData response = await _apiClient.GetAsync<CategoryData>($"/categories/{id}");

                CategoryDTO categoryDTO = new CategoryDTO
                {
                    Id = response.CategoryId,
                    Name = response.CategoryName
                };

                return categoryDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagingDTO<CustomerDTO>> GetCustomers(int pageSize = 100, int currentItem = 1)
        {
            try
            {
                KiotVietResponse<CustomerData> response = await _apiClient.GetAsync<KiotVietResponse<CustomerData>>($"/customers?pageSize={pageSize}&currentItem={currentItem}");

                List<CustomerDTO> customerDTOs = response.Data
                    .Select(item => new CustomerDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Address = item.Address,
                        Birthdate = item.Birthdate,
                        Code = item.Code,
                        Comment = item.Comment,
                        ContactNumber = item.ContactNumber,
                        Debt = item.Debt,
                        Email = item.Email,
                        Gender = item.Gender,
                        LocationName = item.LocationName,
                        Organization = item.Organization,
                        TaxCode = item.TaxCode,
                        TotalInvoiced = item.TotalInvoiced,
                        TotalPoint = item.TotalPoint,
                        TotalRevenue = item.TotalRevenue
                    }).ToList();

                PagingDTO<CustomerDTO> pagingDTO = new PagingDTO<CustomerDTO>
                {
                    Total = response.Total,
                    PageSize = response.PageSize,
                    CurrentItem = currentItem,
                    Data = customerDTOs
                };

                return pagingDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CustomerDTO> GetCustomer(int id)
        {
            try
            {
                CustomerData response = await _apiClient.GetAsync<CustomerData>($"/customers/{id}");

                CustomerDTO customerDTO = new CustomerDTO
                {
                    Id = response.Id,
                    Name = response.Name,
                    Address = response.Address,
                    Birthdate = response.Birthdate,
                    Code = response.Code,
                    Comment = response.Comment,
                    ContactNumber = response.ContactNumber,
                    Debt = response.Debt,
                    Email = response.Email,
                    Gender = response.Gender,
                    LocationName = response.LocationName,
                    Organization = response.Organization,
                    TaxCode = response.TaxCode,
                    TotalInvoiced = response.TotalInvoiced,
                    TotalPoint = response.TotalPoint,
                    TotalRevenue = response.TotalRevenue
                };

                return customerDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CustomerDTO> AddCustomer(CustomerRequest customer)
        {
            try
            {
                KiotVietResponse<CustomerData> customerResponse = await _apiClient.GetAsync<KiotVietResponse<CustomerData>>($"/customers?orderDirection=Desc&orderBy=Id");

                customer.Code = IncrementCustomerCode(customerResponse.Data.FirstOrDefault().Code);

                CustomerReponse result = await _apiClient.PostAsync<CustomerRequest ,CustomerReponse>($"/customers", customer);

                CustomerData response = result.Data;

                CustomerDTO customerDTO = new CustomerDTO
                {
                    Id = response.Id,
                    Name = response.Name,
                    Address = response.Address,
                    Birthdate = response.Birthdate,
                    Code = response.Code,
                    Comment = response.Comment,
                    ContactNumber = response.ContactNumber,
                    Debt = response.Debt,
                    Email = response.Email,
                    Gender = response.Gender,
                    LocationName = response.LocationName,
                    Organization = response.Organization,
                    TaxCode = response.TaxCode,
                    TotalInvoiced = response.TotalInvoiced,
                    TotalPoint = response.TotalPoint,
                    TotalRevenue = response.TotalRevenue
                };

                return customerDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CustomerDTO> UpdateCustomer(CustomerRequest customer, int id)
        {
            try
            {

                CustomerReponse result = await _apiClient.PutAsync<CustomerRequest, CustomerReponse>($"/customers/{id}", customer);

                CustomerData response = result.Data;

                CustomerDTO customerDTO = new CustomerDTO
                {
                    Id = response.Id,
                    Name = response.Name,
                    Address = response.Address,
                    Birthdate = response.Birthdate,
                    Code = response.Code,
                    Comment = response.Comment,
                    ContactNumber = response.ContactNumber,
                    Debt = response.Debt,
                    Email = response.Email,
                    Gender = response.Gender,
                    LocationName = response.LocationName,
                    Organization = response.Organization,
                    TaxCode = response.TaxCode,
                    TotalInvoiced = response.TotalInvoiced,
                    TotalPoint = response.TotalPoint,
                    TotalRevenue = response.TotalRevenue
                };

                return customerDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteCustomer(int id)
        {
            try
            {
                CustomerReponse result = await _apiClient.DeleteAsync<CustomerReponse>($"/customers/{id}");

                return result.Message;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderDTO> AddOrder(OrderDTO orderDTO)
        {
            try
            {
                List<OrderDetailData> orderDetailData = new List<OrderDetailData>();
                foreach (var item in orderDTO.Details)
                {
                    orderDetailData.Add(new OrderDetailData
                    {
                        Note = item.Note,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    });
                }

                DeliveryDetail deliveryDetail = new DeliveryDetail
                {
                    Address = orderDTO.Address,
                    ContactNumber = orderDTO.ContactNumber,
                    LocationName = orderDTO.LocationName,
                    Receiver = orderDTO.Receiver
                };

                OrderRequest orderRequest = new OrderRequest
                {
                    BranchId = orderDTO.BranchId,
                    CustomerId = orderDTO.CustomerId,
                    DeliveryDetail = deliveryDetail,
                    OrderDetails = orderDetailData
                };

                OrderResponse result = await _apiClient.PostAsync<OrderRequest, OrderResponse>($"/orders", orderRequest);

                List<OrderDetailDTO> orderDetailDTOs = new List<OrderDetailDTO>();
                foreach (var item in result.OrderDetails)
                {
                    orderDetailDTOs.Add(new OrderDetailDTO
                    {
                        Note = item.Note,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    });
                }

                OrderDTO order = new OrderDTO
                {
                    OrderId = result.Id,
                    Code = result.Code,
                    Address = result.OrderDelivery.Address,
                    ContactNumber = result.OrderDelivery.ContactNumber,
                    BranchId = result.BranchId,
                    CustomerId = result.CustomerId,
                    LocationName = result.OrderDelivery.LocationName,
                    Receiver = result.OrderDelivery.Receiver,
                    Details = orderDetailDTOs
                };

                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string IncrementCustomerCode(string customerCode)
        {
            if (customerCode.StartsWith("KH") && customerCode.Length > 2)
            {
                string numberPart = customerCode.Substring(2);

                if (int.TryParse(numberPart, out int customerNumber))
                {
                    customerNumber++;

                    return $"KH{customerNumber:D6}";
                }
            }

            return customerCode;
        }

    }
}
