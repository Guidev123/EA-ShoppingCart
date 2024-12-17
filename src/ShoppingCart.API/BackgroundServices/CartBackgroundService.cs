using EA.CommonLib.MessageBus;
using EA.CommonLib.MessageBus.Integration.OrderPlaced;
using ShoppingCart.API.Data.Repositoreis.Interfaces;

namespace ShoppingCart.API.BackgroundServices
{
    public class CartBackgroundService(IMessageBus bus, IServiceProvider serviceProvider) : BackgroundService
    {
        private readonly IMessageBus _bus = bus;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        private void SetSubscribers() =>
            _bus.SubscribeAsync<OrderPlacedIntegrationEvent>("OrderPlaced", DeleteCart);

        private async Task DeleteCart(OrderPlacedIntegrationEvent integrationEvent)
        {
            using var scope = _serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<ICustomerCartRepository>();

            var cart = await repository.GetByCustomerIdAsync(integrationEvent.CustomerId.ToString());
            if(cart.Data is not null && cart.IsSuccess)
                await repository.DeleteAsync(cart.Data);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }
    }
}
