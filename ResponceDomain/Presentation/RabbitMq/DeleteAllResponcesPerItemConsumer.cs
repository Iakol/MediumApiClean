
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ResponceDomain.Presentation.UseCases;
using System.Text;

namespace ResponceDomain.Presentation.RabbitMq
{
    public class DeleteAllResponcesPerItemConsumer : BackgroundService
    {
        private readonly IOptions<RabbitHost> _rabbitHost;
        private const string deleteAllResponcesQueue = "DeleteAllResponcesPerItem";
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IChannel _channel;
        private readonly IServiceScopeFactory _scopeFactory;

        public DeleteAllResponcesPerItemConsumer(IServiceScopeFactory serviceScope , IOptions<RabbitHost> rabbitHost)
        {
            _scopeFactory = serviceScope;
            _rabbitHost = rabbitHost;
            _connectionFactory = new ConnectionFactory
            {
                HostName = _rabbitHost.Value.HostName,
                UserName = _rabbitHost.Value.UserName,
                Port = _rabbitHost.Value.Port,
                Password = _rabbitHost.Value.Password,
            };
        }
        public override Task StartAsync(CancellationToken stoppingToken)
        {
            _connection = _connectionFactory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;

            _channel.QueueDeclareAsync(
                queue: deleteAllResponcesQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                ).Wait();



            return base.StartAsync(stoppingToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            using var scope = _scopeFactory.CreateScope();
            var _deleteResponcePerItemCase = scope.ServiceProvider.GetRequiredService<IDeleteResponcePerItemCase>();
            consumer.ReceivedAsync += async (ch, ea) =>
            {
                string ItemID = Encoding.UTF8.GetString(ea.Body.Span);
                await _channel.BasicAckAsync(ea.DeliveryTag, false);
                await _deleteResponcePerItemCase.Handle(ItemID);

            };

            _channel.BasicConsumeAsync(
                queue: deleteAllResponcesQueue,
                autoAck: false,
                consumer: consumer
                );
            return Task.CompletedTask;

        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.AbortAsync().Wait();
            _connection.AbortAsync().Wait();
            return base.StopAsync(cancellationToken);
        }

    }
}
