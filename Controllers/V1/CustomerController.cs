using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CustomerModels;
using OnlineShop.Dtos;
using OnlineShop.Models;
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


        /// <summary>
        /// Register Customer
        /// </summary>
        /// <param name="model">Mobile & Password</param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Login Customer
        /// </summary>
        /// <param name="login">Mobile & Password</param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(LoginResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] RegisterModel login)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _customerService.PasswordIsCorrect(login.Mobile, login.Password))
            {
                ModelState.AddModelError("model.Mobile", "کاربری یافت نشد .");
                return BadRequest(ModelState);
            }
            var user = await _customerService.Login(login.Mobile, login.Password);
            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}
