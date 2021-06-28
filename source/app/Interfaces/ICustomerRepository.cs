using System.Collections.Generic;
using System.Threading.Tasks;
using app.Exceptions;
using app.Inputs;
using app.Payloads;

namespace app.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerPayload>> GetCustomersAsync();
        Task<CustomerPayload> GetCustomerAsync(int id);
        Task<CustomerPayload> AddCustomerAsync(CustomerInput input);
        Task<CustomerPayload> UpdateCustomerAsync(int id, CustomerInput input);
        Task RemoveCustomerAsync(int id);
    }
}