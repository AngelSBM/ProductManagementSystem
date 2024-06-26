﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Shared.DTOs.Item
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }  
        public bool Active { get; set; }
        public CategoryDto Category { get; set; } = null!;
    }
}
