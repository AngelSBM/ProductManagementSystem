using BusinessLogic.DTOs;
using Flurl;
using Flurl.Http;
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
    public class ItemService : IItemService
    {
        private const string _itemPath = "/Item";
        private const string _baseUrl = "https://localhost:44307";

        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {

            var result = await _baseUrl
                .AppendPathSegment(_itemPath)
                .AppendPathSegment("/Getall")
                .GetJsonAsync<List<ItemDto>>();

            return result;

        }

        public async Task<IEnumerable<CategoryDto>> GetCateogoriesAsync()
        {

            var result = await _baseUrl
                .AppendPathSegment(_itemPath)
                .AppendPathSegment("/GetItemCategories")
                .GetJsonAsync<List<CategoryDto>>();

            return result;

        }

        public async Task<ItemDto> GetItemById(int itemId)
        {

            var result = await _baseUrl
                .AppendPathSegment(_itemPath)
                .AppendPathSegment(itemId)
                .GetJsonAsync<ItemDto>();

            return result;
        }

        public async Task CreateItemAsync(CreateItemDto newItem)
        {
            await _baseUrl
                .AppendPathSegment(_itemPath)
                .AppendPathSegment("Create")
                .PostJsonAsync(newItem);

        }

        public async Task UpdateItemAsync(ItemDto updatedItem)
        {
            await _baseUrl
                .AppendPathSegment(_itemPath)
                .AppendPathSegment("Update")
                .PutJsonAsync(updatedItem);

        }


        public async Task DeleteItemAsync(int id)
        {
            await _baseUrl
                .AppendPathSegment(_itemPath)
                .AppendPathSegment("Delete")
                .AppendPathSegment(id)
                .DeleteAsync();

        }


    }
}
