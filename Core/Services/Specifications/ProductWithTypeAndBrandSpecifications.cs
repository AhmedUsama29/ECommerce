using Domain.Models.Products;
using Shared.DataTransferObjects.Products;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithTypeAndBrandSpecifications : BaseSpecifications<Product>
    {
        //To Get Product By Id
        public ProductWithTypeAndBrandSpecifications(int id) : base(prod => prod.Id == id)
        {
            AddInclude(prod => prod.ProductType);
            AddInclude(prod => prod.ProductBrand);
        }
        //To Get All Products
        public ProductWithTypeAndBrandSpecifications(ProductQueryParameters productQueryParameters) : base(CreateCriterea(productQueryParameters))
        {
            AddInclude(prod => prod.ProductType);
            AddInclude(prod => prod.ProductBrand);
            ApplySorting(productQueryParameters);
            ApplyPagination(productQueryParameters.PageSize,productQueryParameters.PageIndex);
        }

        private static Expression<Func<Product, bool>> CreateCriterea(ProductQueryParameters productQueryParameters)
        {

            return p =>
         (!productQueryParameters.BrandId.HasValue || p.BrandId == productQueryParameters.BrandId.Value) &&
         (!productQueryParameters.TypeId.HasValue || p.TypeId == productQueryParameters.TypeId.Value) &&
         (string.IsNullOrWhiteSpace(productQueryParameters.Search) ||
         p.Name.ToLower().Contains(productQueryParameters.Search.ToLower()));

        }

        private void ApplySorting(ProductQueryParameters productQueryParameters)
        {
            switch (productQueryParameters.ProductSortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
            }
        }
    }
}
