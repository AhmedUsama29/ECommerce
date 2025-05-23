using AutoMapper;
using Domain.Models.Products;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shared.DataTransferObjects.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, opt =>
                                opt.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandResponse>();
            CreateMap<ProductType, TypeResponse>();
        }

    }

    public class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductResponse, string>
    {
        public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.PictureUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
