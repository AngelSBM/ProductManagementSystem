using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.UnitOfWork;
using Domain.Entities;
using ProductManagementSystem.Shared.DTOs;
using ProductManagementSystem.Shared.DTOs.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IItemService
    {
        Task<ItemDto> GetItemByIdAsync(int id);
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();
        Task CreateNewItemAsync(CreateItemDto newItem);
        Task UpdateItemAsync(ItemDto updatedItem);
        Task DeleteItemAsync(int itemId);
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

    }

    public class ItemService(IUnitOfWork _unitOfWork, IMapper _mapper) : IItemService
    {
        public async Task CreateNewItemAsync(CreateItemDto newItem)
        {
            var itemExists = await _unitOfWork.itemRepository.FilterAsync(x => x.Number == newItem.Number);
            if(itemExists.Any())
                throw new InvalidDataException("Item with this number already exists");

            var itemDb = new Item(){
                Name = newItem.Name,
                Number = newItem.Number,
                Description = newItem.Description,
                Active = true                
            };            

            var cateogry = await _unitOfWork.categoryRepository.GetByAsync(x => x.Id == newItem.Category.Id);
            if (cateogry is null)
                throw new InvalidDataException("Cateogry for product doesnt exists");

            itemDb.CategoryId = cateogry.Id;

            await _unitOfWork.itemRepository.AddAsync(itemDb);
        }

        public async Task DeleteItemAsync(int itemId)
        {
            var itemDb = await _unitOfWork.itemRepository.GetByAsync(x => x.Id == itemId);
            if (itemDb is null)
                throw new InvalidOperationException("item not found");

            itemDb.Active = false;
            await _unitOfWork.Complete();
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
        {
            var items = await _unitOfWork.itemRepository.GetAllAsync();

            var itemsDto = _mapper.Map<IEnumerable<ItemDto>>(items);

            return itemsDto;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.categoryRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);

        }

        public async Task<ItemDto> GetItemByIdAsync(int id)
        {
            var item = await _unitOfWork.itemRepository.GetItemByIdAsync(id);

            var itemDto = _mapper.Map<ItemDto>(item);

            return itemDto;
        }

        public async Task UpdateItemAsync(ItemDto updatedItem)
        {
            var item = await _unitOfWork.itemRepository.GetItemByIdAsync(updatedItem.Id);
            if (item is null)
                throw new InvalidOperationException("Item not found");

            item.Name = updatedItem.Name;
            item.Description = updatedItem.Description; 
            item.Number = updatedItem.Number;
            item.Active = updatedItem.Active;
            item.CategoryId = updatedItem.Category.Id;

            var updatedItemNumber = await _unitOfWork.itemRepository.GetByAsync(x => x.Number == updatedItem.Number);
            if (updatedItemNumber is not null && item.Number != updatedItem.Number)
                throw new InvalidDataException("Item with this number already exists");            
     

            await _unitOfWork.Complete();
        }
    }
}
