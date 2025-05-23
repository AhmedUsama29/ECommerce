using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Basket
{
    public class BasketItemDto
    {

        public int Id { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }

        [Range(1, 99999)]
        public decimal Price { get; set; }

        [Range(1,byte.MaxValue)]
        public int Quantity { get; set; }

    }
}
