using InsurancePolicyService.Domain.Entities;
using InsurancePolicyService.Domain.Response;
using InsurancePolicyService.Repository;

namespace InsurancePolicyService.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly AppDbContext _dbContext;
        private readonly IPaymentService paymentService;

        public PurchaseService(AppDbContext dbContext, PaymentService paymentService)
        {
            _dbContext = dbContext;
            this.paymentService = paymentService;
        }
        public async Task<ResponseData<PaymentResponse>> MakePurchaseAsync(Purchase req)
        {
            var custDetails =  _dbContext.ApplicationUsers.Where(x => x.Id == req.UserId).FirstOrDefault();
            decimal premium = Premium(req.BodyType);
            var response = new PaymentResponse();
            try
            {
                var createPayment = await paymentService.InitializeTransactionAsync(custDetails.Email, premium);
                if(createPayment.Status != true)
                {
                    return new ResponseData<PaymentResponse>()
                    {
                        Message = createPayment.Message != null ? "purchase initiated" : "failed initiating purchase",
                        Sucesss = createPayment.Status != null ? true : false
                    };
                }
                var purchase = new PurchaseHistory
                {
                    UserId = req.UserId,
                    Make = req.Make,
                    Model = req.Model,
                    RegistrationNumber = req.RegistrationNumber,
                    BodyType = req.BodyType,
                    RequestId = req.RequestId,
                    Premium = premium,
                    Email = custDetails.Email,
                    PaymentRef = createPayment.Data.Reference,
                    PaymentAccess = createPayment.Data.AccessCode,
                    PaymentStatus = PaymentStatus.Pending,
                };

                _dbContext.History.Add(purchase);
                var result = await _dbContext.SaveChangesAsync();


                return new ResponseData<PaymentResponse>()
                {
                    Data = createPayment,
                    Message = createPayment.Message != null ? "purchase initiated" : "failed  initiating purchase",
                    Sucesss = createPayment.Status != null ? true : false
                };
            }
            catch (Exception ex)
            {
                throw new Exception("purchase failed try again");
            }
        }

        public async void UpdateRecords(WebHookResponse req)
        {
            var record = _dbContext.History.FirstOrDefault(x => x.PaymentRef == req.Data.Reference);
            if(req.Data.Status == "success")
            {
                record.PaymentStatus = PaymentStatus.Paid;

            }
            else
            {
                record.PaymentStatus = PaymentStatus.Cancelled;
            }

            await _dbContext.SaveChangesAsync();

        }

        public async Task<ResponseData<List<PurchaseHistory>>> GetPurchaseHistory(string userId)
        {
            var result = new ResponseData<List<PurchaseHistory>>();
            var record = _dbContext.History.Where(x => x.UserId == userId).ToList();
            if (record.Count <= 0)
            {
                result.Message = "No purchase record found";
                return result;
            }
            else
            {
                result.Data = record;
                return result;
            }
        }

        private decimal Premium(string bodyType)
        {
            switch (bodyType.ToLower())
            {
                case "car":
                    return 15000*100;
                case "suv":
                    return 20000*100;
                case "truck":
                    return 100000*100;
                case "van":
                    return 20000*100;
                default:
                    return 0;
            }
        }
    }
}
