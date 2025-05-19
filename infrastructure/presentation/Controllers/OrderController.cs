using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObjects.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IServiceManager _serviceManager) : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Create(OrderRequest request)
        {

            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await _serviceManager.OrderService.CreateAsync(request, email);

            return Ok(result);

        }

    }
}
