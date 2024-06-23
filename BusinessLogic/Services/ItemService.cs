using BusinessLogic.DTOs;
using ProductManagementSystem.Shared.DTOs;
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

    }

    public class ItemService
    {
    }
}
