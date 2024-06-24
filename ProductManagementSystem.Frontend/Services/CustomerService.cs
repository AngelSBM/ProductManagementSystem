using BusinessLogic.DTOs;
using Flurl;
using Flurl.Http;
using ProductManagementSystem.Shared.DTOs;
using ProductManagementSystem.Shared.DTOs.Item;
using System.Net.Http.Json;

namespace ProductManagementSystem.Frontend.Services
{

    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetCustomersAsync();
        Task<CustomerDto> GetCustomerById(int customerId);
        Task CreateCustomerAsync(CreateCustomerDto newCustomer);
        Task UpdateCustomerAsync(CustomerDto updatedCustomer);
        Task DeleteCustomerAsync(int id);
        Task<CustomerInfoDto> GetCustomerInformationAsync(int customerId);
        Task CreateCustomerItemAsync(CreateCustomerItemDto newCustomerItem);
        Task DeleteCustomerItemAsync(DeleteCustomerItemDto deletedCustomerItem);
    }

    public class CustomerService : ICustomerService
    {        
        private const string _customerPath = "/Customer";
        private const string _baseUrl = "https://localhost:44307";

        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {

            var result = await _baseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("GetAll")
                .GetJsonAsync<List<CustomerDto>>();

            return result;

        }

        public async Task<CustomerDto> GetCustomerById(int customerId)
        {
            var result = await _baseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment(customerId)
                .GetJsonAsync<CustomerDto>();

            return result;
        }

        public async Task CreateCustomerAsync(CreateCustomerDto newCustomer)
        {
            await _baseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("Create")
                .PostJsonAsync(newCustomer);
            
        }

        public async Task UpdateCustomerAsync(CustomerDto updatedCustomer)
        {
            await _baseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("Update")
                .PutJsonAsync(updatedCustomer);

        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _baseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("Delete")
                .AppendPathSegment(id) 
                .DeleteAsync();

        }


        public async Task<CustomerInfoDto> GetCustomerInformationAsync(int customerId)
        {
            var result = await _baseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("GetInformation")
                .AppendPathSegment(customerId)
                .GetJsonAsync<CustomerInfoDto>();

            return result;
        }

        public async Task CreateCustomerItemAsync(CreateCustomerItemDto newCustomerItem)
        {
            await _baseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("CreateCustomerItem")
                .PostJsonAsync(newCustomerItem);
        }

        public async Task DeleteCustomerItemAsync(DeleteCustomerItemDto deletedCustomerItem)
        {
            await _baseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("Delete/CustomerItem")
                .PostJsonAsync(deletedCustomerItem);
        }
    }
}
