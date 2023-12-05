using InsurancePolicyService.Domain.Entities;
using InsurancePolicyService.Domain.Response;

namespace InsurancePolicyService.Services
{
    public interface IPurchaseService
    {
        Task<ResponseData<PaymentResponse>> MakePurchaseAsync(Purchase purchase);
        void UpdateRecords(WebHookResponse req);
        Task<ResponseData<List<PurchaseHistory>>> GetPurchaseHistory(string userId);

    }
}
