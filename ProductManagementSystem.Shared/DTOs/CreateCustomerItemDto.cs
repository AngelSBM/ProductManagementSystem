using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Shared.DTOs
{
    public class CreateCustomerItemDto
    {
        public int CustomerId { get; set; } 
        public int ItemId { get; set; }
    }
}
