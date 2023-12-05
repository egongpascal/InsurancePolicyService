using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyService.Domain.Entities
{
    public class PurchaseHistory
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string UserId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public string BodyType { get; set; }
        public string PaymentRef { get; set; }
        public string PaymentAccess { get; set; }
        public string Email { get; set; }
        public decimal Premium { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
