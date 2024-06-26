﻿using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using ProductManagementSystem.Shared.DTOs;
using ProductManagementSystem.Shared.DTOs.Item;
using QuestPDF.Fluent;
using QuestPDF.Previewer;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class ItemController(IItemService _itemService) : ControllerBase
    {

        [HttpGet]
        [Route("{itemId}")]
        public async Task<IActionResult> GetbyId(int itemId) => Ok(await _itemService.GetItemByIdAsync(itemId));


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _itemService.GetAllItemsAsync());

        [HttpGet]
        [Route("GetItemCategories")]
        public async Task<IActionResult> GetAllCateogires() => Ok(await _itemService.GetCategoriesAsync());

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateItemDto newItem)
        {
            await _itemService.CreateNewItemAsync(newItem);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{itemId}")]
        public async Task<IActionResult> Delete(int itemId)
        {
            await _itemService.DeleteItemAsync(itemId);
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(ItemDto updatedItem)
        {
            await _itemService.UpdateItemAsync(updatedItem);
            return Ok();
        }


    }
}
