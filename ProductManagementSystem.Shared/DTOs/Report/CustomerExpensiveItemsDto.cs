using ProductManagementSystem.Shared.DTOs.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Shared.DTOs.Report
{
    public class CustomerExpensiveItemsDto
    {
        public CustomerDto Customer { get; set; }
        public List<ItemDto> MostExpensiveItems { get; set; } = new List<ItemDto>();
    }
}
