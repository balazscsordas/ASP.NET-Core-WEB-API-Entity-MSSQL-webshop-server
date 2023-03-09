namespace server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public OrderService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        private class Order
        {
            private int BillingDataId { get; set; }
            private int ShippingDataId { get; set; }
            private int PaymentOptionId { get; set; }
            public Order(int billingDataId, int shippingDataId, int paymentOptionId)
            {
                BillingDataId = billingDataId;
                ShippingDataId = shippingDataId;
                PaymentOptionId = paymentOptionId;
            }
        }

        public async Task<ServiceResponse<List<PaymentOptionDto>>> GetPaymentOptions()
        {
            var serviceResponse = new ServiceResponse<List<PaymentOptionDto>>();
            try
            {
                var dbPaymentOptions = await _context.PaymentOptions.ToListAsync();
                if (dbPaymentOptions is null || dbPaymentOptions.Count == 0)
                    throw new Exception("There isn't any payment option.");
                serviceResponse.Data = dbPaymentOptions.Select(o => _mapper.Map<PaymentOptionDto>(o)).ToList();
                serviceResponse.Message = "Successfully fetched all of the payment options.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> PostOrderDetails(OrderDetails orderDetails)
        {
            var serviceResponse = new ServiceResponse<string>();
            try
            {
                var convShippingDetails = _mapper.Map<ShippingData>(orderDetails.shippingDetails);
                var convBillingDetails = _mapper.Map<BillingData>(orderDetails.billingDetails);
                _context.BillingDetails.Add(convBillingDetails);
                _context.ShippingDetails.Add(convShippingDetails);
                _context.SaveChanges();

                Order order = new Order(convBillingDetails.Id, convShippingDetails.Id, orderDetails.paymentDetails.Id);
                Console.WriteLine(convBillingDetails.Id);
                Console.WriteLine(convShippingDetails.Id);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
