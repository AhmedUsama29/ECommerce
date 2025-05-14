using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IAuthenticationService
    {

        Task<UserResponse> LoginAsync(LoginRequest request);
        Task<UserResponse> RegisterAsync(RegisterRequest request);
        Task<bool> CheckEmailAsync(string email);
        Task<AddressDto> GetUserAddress(string email);
        Task<AddressDto> UpdateUserAddressAsync(AddressDto addressDto,string email);

        Task<UserResponse> GetUserByEmail(string email);

    }
}
