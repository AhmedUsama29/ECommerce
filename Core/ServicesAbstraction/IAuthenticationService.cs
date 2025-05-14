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

    }
}
