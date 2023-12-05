
using InsurancePolicyService.Domain.Dto;
using InsurancePolicyService.Domain.Entities;
using InsurancePolicyService.Domain.Response;
using InsurancePolicyService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace InsurancePolicyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService purchase;
        private readonly IPaymentService paymentService;

        public PurchaseController( IPurchaseService purchase, PaymentService paymentService)
        {
            this.purchase = purchase;
            this.paymentService = paymentService;
        }

       
        [HttpPost("HandleWebHook")]
        public async Task<IActionResult> HandleWebHook([FromBody] WebHookResponse requestBody)
        {          
                if (requestBody == null)
                {
                    return BadRequest("Invalid webhook request.");
                }

                try
                {
                    paymentService.VerifyTransactionAsync(requestBody.Data.Reference);

                    purchase.UpdateRecords(requestBody);

                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        
        
        //[HttpPost("HandleWebHook")]
        //public async Task<IActionResult> HandleWebhook()
        //{

        //    using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        //    {
        //        string requestBody = await reader.ReadToEndAsync();
        //        if (requestBody == null)
        //        {
        //            return BadRequest("Invalid webhook request.");
        //        }

        //        var webhookEvent = JsonSerializer.Deserialize<WebHookResponse>(requestBody);

        //        try
        //        {
        //            paymentService.VerifyTransactionAsync(webhookEvent.Data.Reference);

        //            purchase.UpdateRecords(webhookEvent);

        //            return Ok();
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError);
        //        }
        //    }
        //}

        // [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("Purchase")]
        public async Task<ActionResult<ResponseData<List<PurchaseHistory>>>> Purchase(Purchase request)
        {
            var result = await purchase.MakePurchaseAsync(request);
            return Ok(result);
        }

    //    [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetPurchaseHistory")]
        public async Task<ActionResult<ResponseData<List<PurchaseHistory>>>> GetPurchaseHistory(string userId)
        {
            var history = await purchase.GetPurchaseHistory(userId);
            return Ok(history);
        }
    }
}
