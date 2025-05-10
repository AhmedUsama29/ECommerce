using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObjects.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")] //BaseUrl/api/Products
    [ApiController]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {

        //1)Get User Basket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> Get(string Id)
        {

            var basket = await _serviceManager.BasketService.GetAsync(Id);
            return Ok(basket);
        }

        //2)Update User Basket:
        //2.1) Create Basker
        //2.2) Add Item to Basket
        //2.3) Remove Item from Basket
        //2.4) Update Basket Item's Quantity +/-

        [HttpPost]
        public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto)
        {
            var basket = await _serviceManager.BasketService.UpdateAsync(basketDto);
            return Ok(basket);
        }

        [HttpDelete]

        public async Task<ActionResult<bool>> Delete(string Id)
        {
            await _serviceManager.BasketService.DeleteAsync(Id);
            return NoContent();
        }

        //3)Delete User Basket : After Checkout => Empty Basket

    }
}
