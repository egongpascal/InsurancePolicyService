using System.Text.Json.Serialization;

namespace InsurancePolicyService.Domain.Entities
{
    public class Purchase
    {
        public int RequestId { get; set; }
        public string UserId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public string BodyType { get; set; }
    }

    public class PaymentResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public PaymentData Data { get; set; }
    }

    public class PaymentData
    {
        [JsonPropertyName("authorization_url")]

        public string AuthorizationUrl { get; set; }

        [JsonPropertyName("access_code")]

        public string AccessCode { get; set; }

        [JsonPropertyName("reference")]
        public string Reference { get; set; }
    }


    public enum PaymentStatus
    {
        Pending,
        Paid,
        Cancelled
    }
}

