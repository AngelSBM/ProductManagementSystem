using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using ProductManagementSystem.Shared.DTOs;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class CustomerController(ICustomerService _customerService) : ControllerBase
    {
        [HttpGet]
        [Route("{customerId}")]
        public async Task<IActionResult> GetbyId(int customerId) => Ok(await _customerService.GetCustomerByIdAsync(customerId));


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _customerService.GetAllCustomersAsync());

        [HttpGet]
        [Route("GetInformation/{customerId}")]
        public async Task<IActionResult> GetById(int customerId) => Ok(await _customerService.GetCustomersInformationAsync(customerId));

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateCustomerDto newCustomer)
        {
            await _customerService.CreateNewCustomerAsync(newCustomer);
            return  Ok();
        }

        [HttpPost]
        [Route("CreateCustomerItem")]
        public async Task<IActionResult> CreateCustomerItem(CreateCustomerItemDto newCustomerItem)
        {
            await _customerService.CreateCustomerItemAsync(newCustomerItem);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{customerId}")]
        public async Task<IActionResult> Delete(int customerId)
        {
            await _customerService.DeleteCustomerAsync(customerId);
            return Ok();
        }

        [HttpPost]
        [Route("Delete/CustomerItem")]
        public async Task<IActionResult> DeleteCustomerItem(DeleteCustomerItemDto customerItemToDelete)
        {
            await _customerService.DeleteCustomerItemAsync(customerItemToDelete);
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(CustomerDto updatedCustomer)
        {
            await _customerService.UpdateCustomerAsync(updatedCustomer);
            return Ok();
        }

        

    }
}
