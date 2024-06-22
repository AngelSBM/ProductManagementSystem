
using ProductManagementSystem.Shared.DTOs;
using System.Net.Http.Json;

namespace ProductManagement.Frontend.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        public CustomerService(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CustomerDto>>();
        }
    }
}
