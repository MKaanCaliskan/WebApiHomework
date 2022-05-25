using BilgeAdam.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdam.Services.Abstractions
{
    public interface ICustomerService
    {
        PagedList<List<CustomerListDto>> GetPagedCustomers(int count, int page);
        CustomerDto GetCustomerById(string id);
        bool AddNewCustomer(CustomerAddDto dto);
        bool RemoveCustomer(string id);
        bool UpdateCustomer(string id,CustomerAddDto dto);
    }
}
