using AutoMapper;
using Domain.Entities;
using ProductManagementSystem.Shared.DTOs;
using ProductManagementSystem.Shared.DTOs.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<Customer, CustomerInfoDto>();
            CreateMap<Item, ItemDto>().ForMember(x => x.Category, opts => opts.MapFrom(MapCategories));
            CreateMap<Category, CategoryDto>();

        }

        private CategoryDto MapCategories(Item item, ItemDto itemDto)
        {
            if (item.Category is null)
                return new CategoryDto();

            return new CategoryDto()
            {
                Id = item.CategoryId,
                Name = item.Category.Name,
            };
        }
    }
}
