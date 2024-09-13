using NhanhVNServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanhVNServices.Repository
{
    public interface INhanhVNRepository
    {
        Task<RootProduct> GetProducts();
        Task<ProductResponse> GetProduct(int id);
        Task<int> AddOrder(CreateOrderRequest order);
        Task<string> AddCustomer(CustomerRequest customer);
    }
}
