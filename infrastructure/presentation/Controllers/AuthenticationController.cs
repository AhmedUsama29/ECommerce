using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
