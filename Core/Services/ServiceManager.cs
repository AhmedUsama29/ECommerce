using AutoMapper;
using Domain.Contracs;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork,
                                IMapper _mapper,
                                IBasketRepository _basketRepository,
                                UserManager<ApplicationUser> _userManager) : IServiceManager
    {

        private readonly Lazy<IProductService> _lazyProductService
            = new Lazy<IProductService> (() => new ProductService(_unitOfWork,_mapper));
        public IProductService ProductService => _lazyProductService.Value;


        private readonly Lazy<IBasketService> _lazyBasketService
            = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        public IBasketService BasketService => _lazyBasketService.Value;

        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService
            = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager));
        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;

    }
}
