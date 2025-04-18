using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CustomerModels;
using OnlineShop.Services.Customer;

namespace OnlineShop.Controllers.V1
{
    [Route("api/v{version:apiVersion}/customer")]
    [ApiVersion(1.0)]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
             if (!ModelState.IsValid) 
                return BadRequest();

            if (await _customerService.ExistMobile(model.Mobile))
            {
                ModelState.AddModelError("model.Mobile", "شماره موبایل تکراری است");
                return BadRequest(ModelState);
            }

            if (await _customerService.Register(model))
            {
                return StatusCode(201);
            } else
            {
                ModelState.AddModelError("", "مشکلی در سیستم به وجود آمد");
                return BadRequest(ModelState);
            }
        }
    }
}
