using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentService.ObjectStorage;

namespace PaymentService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="paymentRepository">Репозиторий платежей</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        }

        /// <summary>
        /// Создание платежа
        /// </summary>
        /// <returns>Платеж</returns>
        [HttpPost]
        public Payment CreatePayment()
        {
            var result = _paymentRepository.CreatePayment();

            return result;
        }

        /// <summary>
        /// Подтверждение платежа
        /// </summary>
        /// <param name="paymentId">ID платежа</param>
        /// <returns>Платеж</returns>
        [HttpPost]
        [Route("{paymentId}/confirm")]
        public Payment ConfirmPayment([FromRoute] Guid paymentId)
        {
            var result = _paymentRepository.ConfirmPayment(paymentId);

            return result;
        }

        /// <summary>
        /// Отмена платежа
        /// </summary>
        /// <param name="paymentId">ID платежа</param>
        /// <returns>Платеж</returns>
        [HttpPost]
        [Route("{paymentId}/cancel")]
        public Payment CancelPayment([FromRoute] Guid paymentId)
        {
            var result = _paymentRepository.CancelPayment(paymentId);

            return result;
        }
    }
}
