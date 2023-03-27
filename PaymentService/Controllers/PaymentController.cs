using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/payments")]
    public class PaymentController : ControllerBase
    {
        [HttpPost]
        public Payment CreatePayment()
        {
            var result = new PaymentRepository().CreatePayment();

            return result;
        }

        [HttpPost]
        [Route("{paymentId}/confirm")]
        public Payment ConfirmPayment([FromRoute] Guid paymentId)
        {
            var result = new PaymentRepository().ConfirmPayment(paymentId);

            return result;
        }

        [HttpPost]
        [Route("{paymentId}/cancel")]
        public Payment CancelPayment([FromRoute] Guid paymentId)
        {
            var result = new PaymentRepository().CancelPayment(paymentId);

            return result;
        }
    }
}
