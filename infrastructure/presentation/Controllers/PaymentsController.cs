using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServicesAbstraction;
using Shared.DataTransferObjects.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IServiceManager _serviceManager) : ControllerBase
    {

        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdate(string basketId)
        {
            var result = await _serviceManager.PaymentService.CreateOrUpdatePaymentIntent(basketId);

            return Ok(result);
        }


        [HttpPost("WebHook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            //Logic
            await _serviceManager.PaymentService.UpdateOrderPaymentStatus(json
                                                                        , Request.Headers["Stripe-Signature"]);

            return new EmptyResult();
        }


        }
}
