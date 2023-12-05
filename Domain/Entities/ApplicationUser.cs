using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancePolicyService.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Created_At { get; set; }
        public string FirstName { get; set; }
        public DateTime DOB { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
