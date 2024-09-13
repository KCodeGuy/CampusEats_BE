using Microsoft.Extensions.Options;
using NhanhVNServices.Config;
using NhanhVNServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NhanhVNServices.Repository
{
    public class NhanhVNRepository : INhanhVNRepository
    {
        private readonly NhanhVNConfig _config;
        public NhanhVNRepository(IOptions<NhanhVNConfig> nhanhVnConfig)
        {
            _config = nhanhVnConfig.Value;
        }
        public async Task<string> AddCustomer(CustomerRequest customer)
        {
            string apiUrl = "https://open.nhanh.vn/api/customer/add"; 

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                //List<CustomerRequest> customerRequests = new List<CustomerRequest>();
                //customerRequests.Add(customer);

                //string jsonResult = JsonSerializer.Serialize(customerRequests, options);

                var formData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("version", _config.Version),
                        new KeyValuePair<string, string>("appId", _config.AppId),
                        new KeyValuePair<string, string>("businessId", _config.BusinessId),
                        new KeyValuePair<string, string>("accessToken", _config.AccessToken),
                        new KeyValuePair<string, string>("data", "[{\"name\":\"" + customer.Name + "\",\"mobile\":\"" + customer.Mobile + "\"}]")
                    };

                using (var client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(formData);

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();

                        NhanhVNResponse<List<CustomerResponse>> customers = JsonSerializer.Deserialize<NhanhVNResponse<List<CustomerResponse>>>(result, options);

                        return customers.Data.First().Id;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddOrder(CreateOrderRequest order)
        {
            string apiUrl = "https://open.nhanh.vn/api/order/add";

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = new LowercaseNamingPolicy(),
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    PropertyNameCaseInsensitive = true
                };

                //List<CustomerRequest> customerRequests = new List<CustomerRequest>();
                //customerRequests.Add(customer);

                string jsonResult = JsonSerializer.Serialize(order, options);

                var formData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("version", _config.Version),
                        new KeyValuePair<string, string>("appId", _config.AppId),
                        new KeyValuePair<string, string>("businessId", _config.BusinessId),
                        new KeyValuePair<string, string>("accessToken", _config.AccessToken),
                        new KeyValuePair<string, string>("data",jsonResult)
                    };

                using (var client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(formData);

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();

                        NhanhVNResponse<AddOrderResponse> orderResponse = JsonSerializer.Deserialize<NhanhVNResponse<AddOrderResponse>>(result, options);

                        return orderResponse.Data.OrderId;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductResponse> GetProduct(int id)
        {
            string apiUrl = "https://open.nhanh.vn/api/product/detail";

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                //List<CustomerRequest> customerRequests = new List<CustomerRequest>();
                //customerRequests.Add(customer);

                //string jsonResult = JsonSerializer.Serialize(customerRequests, options);

                var formData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("version", _config.Version),
                        new KeyValuePair<string, string>("appId", _config.AppId),
                        new KeyValuePair<string, string>("businessId", _config.BusinessId),
                        new KeyValuePair<string, string>("accessToken", _config.AccessToken),
                        new KeyValuePair<string, string>("data", id + "")
                    };

                using (var client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(formData);

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();

                        ProductDetail products = JsonSerializer.Deserialize<ProductDetail>(result, options);

                        return products.Data.First().Value;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RootProduct> GetProducts()
        {
            string apiUrl = "https://open.nhanh.vn/api/product/search";

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                //List<CustomerRequest> customerRequests = new List<CustomerRequest>();
                //customerRequests.Add(customer);

                //string jsonResult = JsonSerializer.Serialize(customerRequests, options);

                var formData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("version", _config.Version),
                        new KeyValuePair<string, string>("appId", _config.AppId),
                        new KeyValuePair<string, string>("businessId", _config.BusinessId),
                        new KeyValuePair<string, string>("accessToken", _config.AccessToken)
                    };

                using (var client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(formData);

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();

                        NhanhVNResponse<RootProduct> products = JsonSerializer.Deserialize<NhanhVNResponse<RootProduct>>(result, options);

                        return products.Data;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public class LowercaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }
    }
}
