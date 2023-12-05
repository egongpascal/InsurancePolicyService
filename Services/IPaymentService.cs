using InsurancePolicyService.Domain.Entities;
using InsurancePolicyService.Domain.Response;

namespace InsurancePolicyService.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> InitializeTransactionAsync(string customerEmail, decimal amount);
        Task<PaystackVerificationResponse> VerifyTransactionAsync(string reference);

    }
}
