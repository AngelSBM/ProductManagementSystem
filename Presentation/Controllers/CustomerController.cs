using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController(ICustomerService _customerService) : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateCustomerDto newCustomer)
        {
            await _customerService.CreateNewCustomerAsync(newCustomer);
            return  Ok();
        }
        

    }
}
