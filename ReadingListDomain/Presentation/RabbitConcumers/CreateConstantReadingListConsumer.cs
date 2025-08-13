
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReadingListDomain.Presentation.UserCases;
using System.Threading;

namespace ReadingListDomain.Presentation.RabbitConcumers
{
    public class CreateConstantReadingListConsumer : BackgroundService
    {
        private readonly IOptions<RabbitLogCreds> _RabbitOpt;
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IChannel _channel;
        private readonly string CreateConstantReadingListQueue = "CreateConstantReadingListForUserQueue";
        private readonly ICreateConstantReadingListToUserCase _createConstantReadingListToUserCase;

        public CreateConstantReadingListConsumer(IOptions<RabbitLogCreds> options, ICreateConstantReadingListToUserCase createConstantReadingListToUserCase) 
        {
            _RabbitOpt = options;
            _connectionFactory = new ConnectionFactory
            {
                HostName = _RabbitOpt.Value.HostName,
                UserName = _RabbitOpt.Value.UserName,
                Password = _RabbitOpt.Value.Password,
            };

            _createConstantReadingListToUserCase = createConstantReadingListToUserCase;
        }

        public override Task StartAsync(CancellationToken stoppingToken)
        {
            _connection =  _connectionFactory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;

             _channel.QueueDeclareAsync(
                queue: CreateConstantReadingListQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
            return base.StartAsync(stoppingToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (ch, ea) =>
            {
                string UserId = ea.Body.ToString();
                _createConstantReadingListToUserCase.Handle(UserId);

                await _channel.BasicAckAsync(ea.DeliveryTag,false);

            };

            _channel.BasicConsumeAsync(
                queue: CreateConstantReadingListQueue,
                autoAck: false,
                consumer: consumer
                );

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {

            _channel?.CloseAsync();
            _connection?.CloseAsync();
            return base.StopAsync(cancellationToken);
        }

    }
}
