using Shared;
using Shared.DataTransferObjects.Products;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IProductService
    {

        Task<PaginatedResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters productQueryParameters);

        Task<ProductResponse> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandResponse>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResponse>> GetAllTypesAsync();

    }
}
