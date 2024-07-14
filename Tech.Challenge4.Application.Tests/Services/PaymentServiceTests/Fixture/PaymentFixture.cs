using Tech.Challenge4.Domain.Models.Payment;

namespace Tech.Challenge4.Domain.Tests.Services.PaymentServiceTests.Fixture
{
    public static class PaymentFixture
    {

        public static PaymentModel CreatePaymentModel()
        {
            return new PaymentModel
            {
                CustomerId = 1,
                ReservationId = 1,
                ReservationPaymentValue = 100
            };
        }

    }
}
