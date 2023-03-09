using Microsoft.AspNetCore.Mvc;
using server.Services.OrderService;
using System.Collections.Generic;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public class OrderDetails
        {
            public ShippingDataDto shippingDetails { get; set; }
            public BillingDataDto billingDetails { get; set; }
            public PaymentOption paymentDetails { get; set; }
            public List<ProductFromOrderDto> cartProducts { get; set; }
        }

        [HttpGet("GetPaymentOptions")]
        public async Task<ActionResult<ServiceResponse<List<PaymentOptionDto>>>> GetPaymentOptions()
        {
            return Ok(await _orderService.GetPaymentOptions());
        }

        [HttpPost("PostOrderDetails")]
        public async Task<ActionResult<ServiceResponse<string>>> PostOrderDetails([FromBody] OrderDetails orderDetails)
        {
            Console.WriteLine("joasdjio");
            return Ok(await _orderService.PostOrderDetails(orderDetails));
        }
    }
}
