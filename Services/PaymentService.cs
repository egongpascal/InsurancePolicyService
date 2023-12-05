using InsurancePolicyService.Domain.Entities;
using InsurancePolicyService.Domain.Response;
using InsurancePolicyService.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class PaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;
    private readonly string _secretKey;

    public PaymentService(string secretKey)
    {
        _httpClient = new HttpClient();
        _secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
    }

    public async Task<PaymentResponse> InitializeTransactionAsync(string customerEmail, decimal amount)
    {
        string url = "https://api.paystack.co/transaction/initialize";

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_secretKey}");

        var data = new
        {
            email = customerEmail,
            amount = amount.ToString("F2")
        };

        var jsonData = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var responseJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PaymentResponse>(responseJson);
            return result;
        }
        else
        {
            throw new HttpRequestException($"Error: {response.StatusCode} - {response.ReasonPhrase}");
        }
    }

    public async Task<PaystackVerificationResponse> VerifyTransactionAsync(string reference)
    {
        string url = $"https://api.paystack.co/transaction/verify/{reference}";

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretKey);

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<PaystackVerificationResponse>(responseJson);
                return result;
            }
            else
            {
                throw new HttpRequestException($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            throw new HttpRequestException($"Exception: {ex.Message}");
        }
    }
}
