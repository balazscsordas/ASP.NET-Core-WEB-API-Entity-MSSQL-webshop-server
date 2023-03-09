global using static server.Controllers.OrderController;

namespace server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<PaymentOptionDto>>> GetPaymentOptions();
        Task<ServiceResponse<string>> PostOrderDetails(OrderDetails orderDetails);

    }
}
