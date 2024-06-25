using ProductManagementSystem.Shared.DTOs.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Shared.DTOs.Report
{
    public class CustomerItemRangeItemDto
    {
        public CustomerDto Customer { get; set; }
        public ItemDto Item { get; set; }
    }
}
