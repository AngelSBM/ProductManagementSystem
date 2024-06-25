using BusinessLogic.DTOs;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using ProductManagementSystem.Frontend.Helpers;
using ProductManagementSystem.Shared.DTOs;
using ProductManagementSystem.Shared.DTOs.Item;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

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

    public class CustomerService(ILoadingService loadingService, IAccessTokenProvider tokenProvider, ConfigurationSettings configurationSettings) : ICustomerService
    {        
        private const string _customerPath = "/Customer";

        public async Task<string> GetTokenAsync()
        {
            string token = "";
            var tokenResult = await tokenProvider.RequestAccessToken();
            if (tokenResult.TryGetToken(out var tokenResponse)) 
            {
                token = tokenResponse.Value;
            }

            return token;
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            var token = await GetTokenAsync();

            var result = await configurationSettings.BaseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("GetAll") 
                .GetWithLoadingAsync<List<CustomerDto>>(token, loadingService);
            
            return result;

        }

        public async Task<CustomerDto> GetCustomerById(int customerId)
        {
            var token = await GetTokenAsync();

            var result = await configurationSettings.BaseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment(customerId)
                .GetWithLoadingAsync<CustomerDto>(token, loadingService);

            return result;
        }

        public async Task CreateCustomerAsync(CreateCustomerDto newCustomer)
        {
            var token = await GetTokenAsync();

            await configurationSettings.BaseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("Create")
                .PostWithLoadingAsync(newCustomer, token, loadingService);
                
        }

        public async Task UpdateCustomerAsync(CustomerDto updatedCustomer)
        {
            var token = await GetTokenAsync();

            await configurationSettings.BaseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("Update")
                .WithHeader("Authorization", $"bearer {token}")
                .PutJsonAsync(updatedCustomer);

        }

        public async Task DeleteCustomerAsync(int id)
        {
            var token = await GetTokenAsync();

            await configurationSettings.BaseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("Delete")
                .AppendPathSegment(id) 
                .DeleteWithLoadingAsync(token, loadingService);

        }


        public async Task<CustomerInfoDto> GetCustomerInformationAsync(int customerId)
        {
            var token = await GetTokenAsync();

            var result = await configurationSettings.BaseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("GetInformation")
                .AppendPathSegment(customerId)
                .GetWithLoadingAsync<CustomerInfoDto>(token, loadingService);

            return result;
        }

        public async Task CreateCustomerItemAsync(CreateCustomerItemDto newCustomerItem)
        {
            var token = await GetTokenAsync();
            await configurationSettings.BaseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("CreateCustomerItem")
                .PostWithLoadingAsync(newCustomerItem, token, loadingService);
        }

        public async Task DeleteCustomerItemAsync(DeleteCustomerItemDto deletedCustomerItem)
        {
            var token = await GetTokenAsync();
            await configurationSettings.BaseUrl
                .AppendPathSegment(_customerPath)
                .AppendPathSegment("Delete/CustomerItem")
                .PostWithLoadingAsync(deletedCustomerItem, token, loadingService);
        }
    }
}
