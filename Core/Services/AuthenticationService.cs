using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using ServicesAbstraction;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email) ??
                throw new UserNotFoundException(request.Email);

            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (isValidPassword) return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "JWTTOKEN"
            };

            throw new UnAuthorizedException();

        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                DisplayName = request.DisplayName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded) return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "JWTTOKEN"
            };

            var Errors = result.Errors.Select(e => e.Description).ToList();

            throw new BadRequestException(Errors);

        }
    }
}
