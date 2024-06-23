using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Shared.DTOs.Item
{
    public class CustomerInfoDto : CustomerDto
    {
    public List<ItemDto> Items { get; set; } = new List<ItemDto>();
    }
}
