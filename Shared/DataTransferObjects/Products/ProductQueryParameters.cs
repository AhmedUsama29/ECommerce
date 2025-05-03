using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Products
{
    public class ProductQueryParameters
    {

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions ProductSortingOptions { get; set; }
        public string? Search { get; set; }
    }
}
