using ProductManagementSystem.Shared.DTOs;

namespace ProductManagement.Frontend.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetCustomersAsync();
    }

}
