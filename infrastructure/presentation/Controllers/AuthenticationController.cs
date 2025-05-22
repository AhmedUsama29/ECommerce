using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")] //BaseUrl/api/Products
    [ApiController]
    public class AuthenticationController(IServiceManager _serviceManager) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest request)
        {
            return Ok(await _serviceManager.AuthenticationService.LoginAsync(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest request)
        {
            return Ok(await _serviceManager.AuthenticationService.RegisterAsync(request));
        }


        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            return Ok(await _serviceManager.AuthenticationService.CheckEmailAsync(email));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetUser()
        {
            var mail = User.FindFirstValue(ClaimTypes.Email);

            return Ok(await _serviceManager.AuthenticationService.GetUserByEmail(mail));
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var mail = User.FindFirstValue(ClaimTypes.Email);

            return Ok(await _serviceManager.AuthenticationService.GetUserAddress(mail));
        }


        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
        {
            var mail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManager.AuthenticationService.UpdateUserAddressAsync(address, mail));
        }

    }
}
