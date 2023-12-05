using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancePolicyService.Domain.Dto
{
    public class RegisterResponseDto
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public string UserId { get; set; }
    }
}
