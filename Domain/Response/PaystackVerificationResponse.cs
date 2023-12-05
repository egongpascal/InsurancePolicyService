
using System;
using System.Collections.Generic;

namespace InsurancePolicyService.Domain.Response
{
    public class PaystackVerificationResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public PaystackVerificationData Data { get; set; }
    }

    public class PaystackVerificationData
    {
        public long Id { get; set; }
        public string? Domain { get; set; }
        public string? Status { get; set; }
        public string? Reference { get; set; }
        public decimal? Amount { get; set; }
        public string? GatewayResponse { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Channel { get; set; }
        public string? Currency { get; set; }
        public string? IpAddress { get; set; }
        public string? Metadata { get; set; }
        public PaystackVerificationLog? Log { get; set; }
        public decimal? Fees { get; set; }
        public object? FeesSplit { get; set; }
        public PaystackAuthorization? Authorization { get; set; }
        public PaystackCustomer? Customer { get; set; }
        public object? Plan { get; set; }
        public Dictionary<string, object>? Split { get; set; }
        public object? OrderId { get; set; }
        public decimal? RequestedAmount { get; set; }
        public object? PosTransactionData { get; set; }
        public object? Source { get; set; }
        public object? FeesBreakdown { get; set; }
        public DateTime? TransactionDate { get; set; }
        public Dictionary<string, object>? PlanObject { get; set; }
        public Dictionary<string, object>? Subaccount { get; set; }
    }

    public class PaystackVerificationLog
    {
        public int? StartTime { get; set; }
        public int? TimeSpent { get; set; }
        public int? Attempts { get; set; }
        public int? Errors { get; set; }
        public bool? Success { get; set; }
        public bool? Mobile { get; set; }
        public List<PaystackLogHistory>? History { get; set; }
    }

    public class PaystackLogHistory
    {
        public string? Type { get; set; }
        public string? Message { get; set; }
        public int? Time { get; set; }
    }

    public class PaystackAuthorization
    {
        public string? AuthorizationCode { get; set; }
        public string? Bin { get; set; }
        public string? Last4 { get; set; }
        public string? ExpMonth { get; set; }
        public string? ExpYear { get; set; }
        public string? Channel { get; set; }
        public string? CardType { get; set; }
        public string? Bank { get; set; }
        public string? CountryCode { get; set; }
        public string? Brand { get; set; }
        public bool? Reusable { get; set; }
        public string? Signature { get; set; }
        public object? AccountName { get; set; }
    }

    public class PaystackCustomer
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? CustomerCode { get; set; }
        public string? Phone { get; set; }
        public object? Metadata { get; set; }
        public string? RiskAction { get; set; }
        public object? InternationalFormatPhone { get; set; }
    }

}
