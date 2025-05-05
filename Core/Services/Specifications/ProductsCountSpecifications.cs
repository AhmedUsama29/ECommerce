using Domain.Contracs;
using Domain.Models;
using Shared.DataTransferObjects.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductsCountSpecifications(ProductQueryParameters productQueryParameters) 
        : BaseSpecifications<Product>(CreateCriterea(productQueryParameters))
    {


        private static Expression<Func<Product, bool>> CreateCriterea(ProductQueryParameters productQueryParameters)
        {

            return p =>
         (!productQueryParameters.BrandId.HasValue || p.BrandId == productQueryParameters.BrandId.Value) &&
         (!productQueryParameters.TypeId.HasValue || p.TypeId == productQueryParameters.TypeId.Value) &&
         (string.IsNullOrWhiteSpace(productQueryParameters.Search) ||
         p.Name.ToLower().Contains(productQueryParameters.Search.ToLower()));

        }

    }
}
