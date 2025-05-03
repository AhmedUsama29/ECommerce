using Shared.DataTransferObjects.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IProductService
    {

        Task<IEnumerable<ProductResponse>> GetAllProductsAsync();

        Task<ProductResponse> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandResponse>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResponse>> GetAllTypesAsync();

    }
}
