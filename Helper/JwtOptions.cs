using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancePolicyService.Helper
{
    public static class JwtOptions
    {

        public const string Issuer = "http://localhost:9980";

        public const string Audience = "http://localhost:5000";

        public const string Key = "supersecret_!@@#!!!!=--=-90-556872%$#$#$%@$^&%^[][gg00gvtftf1!!@@%^^^secretkey!12345";
        public const int JwtExpireDays = 30;
    }
}
