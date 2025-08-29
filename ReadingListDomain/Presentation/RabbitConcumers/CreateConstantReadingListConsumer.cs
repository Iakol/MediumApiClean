
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReadingListDomain.Application.DTO;
using ReadingListDomain.Presentation.UserCases;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Channels;

namespace ReadingListDomain.Presentation.RabbitConcumers
{
    public class CreateConstantReadingListConsumer : BackgroundService
    {
        private readonly IOptions<RabbitLogCreds> _RabbitOpt;
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IChannel _channel;
        private const string CreateConstantReadingListQueue = "CreateConstantReadingListForUserQueue";
        private readonly ICreateConstantReadingListToUserCase _createConstantReadingListToUserCase;
        private readonly IDeleteConstantReadingListToUserCase _deleteConstantReadingListToUserCase;

        private const string DeleteConstantReadingListQueue = "DeleteConstantReadingListForUserQueue";
        


        public CreateConstantReadingListConsumer(IOptions<RabbitLogCreds> options, ICreateConstantReadingListToUserCase createConstantReadingListToUserCase) 
        {
            _RabbitOpt = options;
            _connectionFactory = new ConnectionFactory
            {
                HostName = _RabbitOpt.Value.HostName,
                UserName = _RabbitOpt.Value.UserName,
                Password = _RabbitOpt.Value.Password,
                Port = _RabbitOpt.Value.Port,
            };

            _createConstantReadingListToUserCase = createConstantReadingListToUserCase;
        }

        public override Task StartAsync(CancellationToken stoppingToken)
        {
            _connection =  _connectionFactory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
            _channel.QueueDeclareAsync(DeleteConstantReadingListQueue, true, false, false).Wait();

            _channel.QueueDeclareAsync(
                queue: CreateConstantReadingListQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                ).Wait();

            _channel.BasicReturnAsync += (sender, args) =>
            {
                if (args.BasicProperties.ReplyTo.StartsWith("ResultCreateConstantReadingListForUserQueue"))
                {

                    args.BasicProperties.Headers.TryGetValue("userId", out object Value);
                    string userId = (string)Value!;
                    _deleteConstantReadingListToUserCase.Handle(userId).Wait();
                }
                return Task.CompletedTask;
            };

            return base.StartAsync(stoppingToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (ch, ea) =>
            {
                string UserId = Encoding.UTF8.GetString(ea.Body.Span);
                await _channel.BasicAckAsync(ea.DeliveryTag, false);

                Result result = await _createConstantReadingListToUserCase.Handle(UserId);
                CreateUserSageResult RabbitResult;
                if (result.IsSuccess)
                {
                    RabbitResult = CreateUserSageResult.Success(0);
                }
                else 
                {
                    RabbitResult = CreateUserSageResult.Failure(result.Error, 2);

                }

                string RabitResultJson = JsonSerializer.Serialize(RabbitResult);
                var RabitResultMessage = Encoding.UTF8.GetBytes(RabitResultJson);
                var prop = new BasicProperties()
                {
                    Headers = new Dictionary<string, object>() { { "userId", (object)UserId } }
                };
                await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: ea.BasicProperties.ReplyTo, mandatory: true,basicProperties: prop, body: RabitResultMessage);
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
