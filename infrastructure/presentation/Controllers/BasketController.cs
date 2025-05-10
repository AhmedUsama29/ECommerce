using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
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

        //2)Update User Basket:
        //2.1) Create Basker
        //2.2) Add Item to Basket
        //2.3) Remove Item from Basket
        //2.4) Update Basket Item's Quantity +/-

        //3)Delete User Basket : After Checkout => Empty Basket

    }
}
