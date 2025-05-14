using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Authentication
{
    public class UserResponse
    {

        public string Token { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }

    }
}
