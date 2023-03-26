using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("/payments")]
    public class PaymentController : ControllerBase
    {
        [HttpPost]
        public Payment CreatePayment()
        {
            var result = new PaymentRepository().CreatePayment();

            return result;
        }

        [HttpPut]
        [Route("confirm/{paymentId}")]
        public Payment ConfirmPayment(Guid paymentId)
        {
            var result = new PaymentRepository().ConfirmPayment(paymentId);

            return result;
        }

        [HttpPut]
        [Route("cancel/{paymentId}")]
        public Payment CancelPayment(Guid paymentId)
        {
            var result = new PaymentRepository().CancelPayment(paymentId);

            return result;
        }
    }
}
