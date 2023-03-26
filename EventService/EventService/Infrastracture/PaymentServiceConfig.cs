namespace EventService.Infrastracture
{
    public class PaymentServiceConfig
    {
        public string Address { get; set; }

        public string CreatePaymentEndpoint { get; set; }

        public string ConfirmPaymentEndpoint { get; set; }

        public string CancelPaymentEndpoint { get; set; }
    }
}
