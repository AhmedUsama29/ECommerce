﻿using AutoMapper;
using Domain.Contracs;
using Domain.Exceptions;
using Domain.Models.Products;
using Services.Specifications;
using ServicesAbstraction;
using Shared;
using Shared.DataTransferObjects.Products;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResponse>> GetAllBrandsAsync()
        {
            var repository = _unitOfWork.GetRepository<ProductBrand, int>();

            var brands = await repository.GetAllAsync();

            var brandsResponse = _mapper.Map<IEnumerable<BrandResponse>>(brands);

            return brandsResponse;
        }

        public async Task<PaginatedResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters productQueryParameters)
        {

            var specs = new ProductWithTypeAndBrandSpecifications(productQueryParameters);

            var repository = _unitOfWork.GetRepository<Product, int>();

            var products = await repository.GetAllAsync(specs);

            var productsResponse = _mapper.Map<IEnumerable<ProductResponse>>(products);

            var CountSpecs = new ProductsCountSpecifications(productQueryParameters);
            var totalCount = await repository.CountAsync(CountSpecs);

            var res = new PaginatedResponse<ProductResponse>()
            {
                Data = productsResponse,
                PageIndex = productQueryParameters.PageIndex,
                PageSize = productQueryParameters.PageSize,
                Count = totalCount
            };

            return res;

        }

        public async Task<IEnumerable<TypeResponse>> GetAllTypesAsync()
        {
            var repository = _unitOfWork.GetRepository<ProductType, int>();

            var types = await repository.GetAllAsync();

            var typesResponse = _mapper.Map<IEnumerable<TypeResponse>>(types);

            return typesResponse;
        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {

            var specs = new ProductWithTypeAndBrandSpecifications(id);

            var repository = _unitOfWork.GetRepository<Product, int>();

            var product = await repository.GetByIdAsync(specs) ?? 
                throw (new ProductNotFoundException(id));

            var productResponse = _mapper.Map<ProductResponse>(product);

            return productResponse;
        }
    }
}
