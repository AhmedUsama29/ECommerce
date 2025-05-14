using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {

        public string DisplayName { get; set; }

        public Address Address { get; set; }

    }

    public class  Address
    {

        public int Id { get; set; } //string
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
