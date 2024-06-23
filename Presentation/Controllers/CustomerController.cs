using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Shared.DTOs;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpDelete]
        [Route("Delete/{customerId}")]
        public async Task<IActionResult> Delete(int customerId)
        {
            await _customerService.DeleteCustomerAsync(customerId);
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
