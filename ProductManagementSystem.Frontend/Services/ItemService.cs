using BusinessLogic.DTOs;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using ProductManagementSystem.Frontend.Helpers;
using ProductManagementSystem.Shared.DTOs;
using ProductManagementSystem.Shared.DTOs.Item;

namespace ProductManagementSystem.Frontend.Services
{
    public interface IItemService
    {
        Task<ItemDto> GetItemById(int itemId);
        Task<IEnumerable<ItemDto>> GetItemsAsync();
        Task<IEnumerable<CategoryDto>> GetCateogoriesAsync();
        Task CreateItemAsync(CreateItemDto newItem);
        Task UpdateItemAsync(ItemDto updatedItem);
        Task DeleteItemAsync(int id);
    }
    public class ItemService(ILoadingService loadingService, IAccessTokenProvider tokenProvider, ConfigurationSettings configurationSettings) : IItemService
    {
        private const string _itemPath = "/Item";

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

        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var token = await GetTokenAsync();
            var result = await configurationSettings.BaseUrl
                .AppendPathSegment(_itemPath)
                .AppendPathSegment("/Getall")
                .GetWithLoadingAsync<List<ItemDto>>(token,loadingService);

            return result;

        }

        public async Task<IEnumerable<CategoryDto>> GetCateogoriesAsync()
        {
            var token = await GetTokenAsync();
            var result = await configurationSettings.BaseUrl
                .AppendPathSegment(_itemPath)
                .AppendPathSegment("/GetItemCategories")
                .GetWithLoadingAsync<List<CategoryDto>>(token,loadingService);

            return result;

        }

        public async Task<ItemDto> GetItemById(int itemId)
        {
            var token = await GetTokenAsync();
            var result = await configurationSettings.BaseUrl
                .AppendPathSegment(_itemPath)
                .AppendPathSegment(itemId)
                .GetWithLoadingAsync<ItemDto>(token,loadingService);

            return result;
        }

        public async Task CreateItemAsync(CreateItemDto newItem)
        {
            var token = await GetTokenAsync();
            await configurationSettings.BaseUrl
                .AppendPathSegment(_itemPath)
                .AppendPathSegment("Create")
                .PostWithLoadingAsync(newItem,token, loadingService);

        }

        public async Task UpdateItemAsync(ItemDto updatedItem)
        {
            var token = await GetTokenAsync();
            await configurationSettings.BaseUrl
                .AppendPathSegment(_itemPath)
                .AppendPathSegment("Update")
                .PutJsonAsync(updatedItem);

        }


        public async Task DeleteItemAsync(int id)
        {
            var token = await GetTokenAsync();
            await configurationSettings.BaseUrl
                .AppendPathSegment(_itemPath)
                .AppendPathSegment("Delete")
                .AppendPathSegment(id)
                .DeleteWithLoadingAsync(token,loadingService);

        }


    }
}
