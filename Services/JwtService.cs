using InsurancePolicyService.Domain;
using InsurancePolicyService.Helper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InsurancePolicyService.Application.Services
{
    public class JwtService : IJwtService
    {
        public string GenerateToken(UserDto user)
        {

            var jwtSettings = new JwtSettings();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
            var token = new JwtSecurityToken(jwtSettings.Issuer,
                jwtSettings.Issuer,
                claims,
                expires: DateTime.Now.AddDays(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class JwtSettings
        {
            public string SecretKey = "D278KqfyAAt6zLh4dE9J3XQW18dhd3C5euCLDOsRxLvQAkLttmh74RffgE2RqRiHqlsWkfqpdvmhKpyvOt2aWoi71lngqTZp5ZZO";
            public string Issuer = "Insure";
        }
    }
   
    public interface IJwtService
    {
        public string GenerateToken(UserDto user);

    }
}
