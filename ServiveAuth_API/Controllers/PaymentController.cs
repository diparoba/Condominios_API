using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ServiceAuth_API.Models;
using ServiceAuth_API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAuth_API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("PolicyLocal")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IServicePayment _servicePayment;

        public PaymentController(IServicePayment servicePayment)
        {
            _servicePayment = servicePayment;
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(Payment payment)
        {
            var createdPayment = await _servicePayment.AddPaymentAsync(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = createdPayment.Id }, createdPayment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(ObjectId id)
        {
            await _servicePayment.DeletePaymentAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _servicePayment.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(ObjectId id)
        {
            var payment = await _servicePayment.GetPaymentByIdAsync(id);
            return Ok(payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(ObjectId id, Payment payment)
        {
            await _servicePayment.UpdatePaymentAsync(id, payment);
            return Ok();
        }
    }
}
