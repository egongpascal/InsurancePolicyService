
using InsurancePolicyService.Domain.Dto;
using InsurancePolicyService.Services;
using Microsoft.AspNetCore.Mvc;


namespace InsurancePolicyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserDto registerUserDto)
        {
            var registerResponse = await _authService.RegisterUser(registerUserDto);

            if (!registerResponse.Sucesss)
            {
                return StatusCode(400, registerResponse);
            }
            return StatusCode(200, registerResponse);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
        {
            var registerResponse = await _authService.LoginUser(loginDto);

            if (!registerResponse.Sucesss)
            {
                return StatusCode(400, registerResponse);
            }
            return StatusCode(200, registerResponse);
        }
    }
}
