using InsurancePolicyService.Application.Services;
using InsurancePolicyService.Domain.Dto;
using InsurancePolicyService.Domain.Entities;
using InsurancePolicyService.Domain.Response;
using Microsoft.AspNetCore.Identity;

namespace InsurancePolicyService.Services
{
    public class AuthService : IAuthService
    {
        private UserManager<ApplicationUser> _usermanager;
        private SignInManager<ApplicationUser> _signInManger;
        private IJwtService _jwtService;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtService jwtService)
        {
            _usermanager = userManager;
            _signInManger = signInManager;
            _jwtService = jwtService;
        }

        public async Task<ResponseData<RegisterResponseDto>> RegisterUser(RegisterUserDto registerUserDto)
        {
            var regData = new ResponseData<RegisterResponseDto>();
            var user = new ApplicationUser()
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Email = registerUserDto.Email,
                UserName = registerUserDto.Email,
                PhoneNumber = registerUserDto.PhoneNumber,
                DOB = registerUserDto.DOB
            };

            var registerResponse = await _usermanager.CreateAsync(user, registerUserDto.Password);
            var userDetails = _usermanager.Users.Where(x => x.Email == registerUserDto.Email).FirstOrDefault();
           // this.logger.LogInformation($"done calling the DB- {JsonConvert.SerializeObject(registerResponse)}");
            if (registerResponse.Succeeded)
            {
                regData = new ResponseData<RegisterResponseDto>()
                {
                    Sucesss = true,
                    Data = new RegisterResponseDto() { Email = registerUserDto.Email, FirstName = registerUserDto.FirstName, LastName = registerUserDto.LastName },
                    Message = "Registered user Successful"
                };
            }
            else
            {
                regData = new ResponseData<RegisterResponseDto>()
                {
                    Sucesss = false,
                    Data = new RegisterResponseDto() { Email = registerUserDto.Email, FirstName = registerUserDto.FirstName, LastName = registerUserDto.LastName },
                    Message = "Invalid User Detail"
                };
            }
            return (regData);
        }

        public async Task<ResponseData<LoginResponseDto>> LoginUser(LoginDto loginDto)
        {
            var user = await _usermanager.FindByEmailAsync(loginDto.Email);
            Console.WriteLine(user);
            if (user == null)
            {
                return new ResponseData<LoginResponseDto>()
                {
                    Sucesss = false,
                    Message = "Invalid User Detail"
                };
            }

            var signReponse = await _signInManger.PasswordSignInAsync(loginDto.Email, loginDto.Password, true, false);

            if (signReponse.Succeeded)
            {
                var tokenResult = _jwtService.GenerateToken(new Domain.UserDto() { Email = loginDto.Email, Id = user.Id });

                return new ResponseData<LoginResponseDto>()
                {
                    Sucesss = true,
                    Message = "Sucess",
                    Data = new LoginResponseDto { Token = tokenResult, Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email }
                };
            }

            return new ResponseData<LoginResponseDto>()
            {
                Sucesss = false,
                Message = "Invalid User Detail"
            };
        }
    }
}
