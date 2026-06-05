using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartingService.BLL.Interfaces;
using MassTransit;
using Shared.Contracts;

namespace CartingService.BLL.Services
{
    public class ProductUpdatedConsumer : IConsumer<ProductUpdatedEvent>
    {
        private readonly ICartService _cartService;

        public ProductUpdatedConsumer(ICartService cartService)
        {
            _cartService = cartService;
        }

        public Task Consume(ConsumeContext<ProductUpdatedEvent> context)
        {
            var message = context.Message;

            _cartService.UpdateItemInAllCartsAsync(message.ProductId, message.NewName, message.NewPrice);

            return Task.CompletedTask;
        }
    }
}
