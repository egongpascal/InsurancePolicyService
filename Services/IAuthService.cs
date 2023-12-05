using InsurancePolicyService.Domain.Dto;
using InsurancePolicyService.Domain.Response;

namespace InsurancePolicyService.Services
{
    public interface IAuthService
    {
        Task<ResponseData<RegisterResponseDto>> RegisterUser(RegisterUserDto registerUserDto);
        Task<ResponseData<LoginResponseDto>> LoginUser(LoginDto loginDto);
    }
}
