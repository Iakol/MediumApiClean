using IndentityDomain.Application.DTO;
using IndentityDomain.Infrastructure.RabbitMQ.RabbitInitiate;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReadingListDomain.Application.DTO;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace IndentityDomain.Infrastructure.RabbitMQ.Sages
{
    public class CreateUserSaga
    {
        private readonly RabbitFactory _rabbitFactory;
        private static readonly string ResultCreateConstantReadingListForUserQueue = "ResultCreateConstantReadingListForUserQueue" + Guid.NewGuid().ToString();
        private static readonly string ResultCreateUserConstantDataQueue = "ResultCreateUserConstantDataQueue" + Guid.NewGuid().ToString();
        private const string CreateConstantReadingListQueue = "CreateConstantReadingListQueue";
        private const string CreateUserConstantDataQueue = "CreateUserConstantDataQueue";
        private const string DeleteConstantReadingListQueue = "DeleteConstantReadingListForUserQueue";
        private const string DeleteUserConstantDataQueue = "DeleteUserConstantDataQueue";
        private readonly RabbitTaskDictonary _rabbitTaskDictonary;

        private IChannel _channel;

        static bool isDeclareQueues = false;

        public CreateUserSaga(RabbitFactory rabbitFactory, RabbitTaskDictonary rabbitTaskDictonary)
        {
            _rabbitFactory = rabbitFactory;
            _rabbitTaskDictonary = rabbitTaskDictonary;
            _channel = _rabbitFactory.GetConnectionChanel();

            if (!isDeclareQueues) { 
                _channel.QueueDeclareAsync(ResultCreateConstantReadingListForUserQueue, false, true, true).Wait();
                _channel.QueueDeclareAsync(ResultCreateUserConstantDataQueue, false, true, true).Wait();
                _channel.QueueDeclareAsync(CreateConstantReadingListQueue, true, false, false, _rabbitFactory.CreateDeadLeaterArgumentsForQueue(ResultCreateConstantReadingListForUserQueue, 20000)).Wait();
                _channel.QueueDeclareAsync(CreateUserConstantDataQueue, true, false, false, _rabbitFactory.CreateDeadLeaterArgumentsForQueue(ResultCreateUserConstantDataQueue, 20000)).Wait();
                _channel.QueueDeclareAsync(DeleteConstantReadingListQueue, true, false, false).Wait();
                _channel.QueueDeclareAsync(DeleteUserConstantDataQueue, true, false, false).Wait();

                _channel.BasicReturnAsync += (sender, args) =>
                {
                    string routingKey = args.RoutingKey;
                    if (
                        routingKey.Equals(CreateConstantReadingListQueue) ||
                        routingKey.Equals(CreateUserConstantDataQueue) ||
                        routingKey.Equals(DeleteConstantReadingListQueue) ||
                        routingKey.Equals(DeleteUserConstantDataQueue)
                    )
                    {
                        string reason = args.ReplyText;

                        if (reason.Equals("NO_ROUTE"))
                        {
                            string? correlationId = args.BasicProperties?.CorrelationId;
                            _channel.QueueDeclareAsync(routingKey, true, false, false).Wait();

                            if (
                            routingKey.Equals(DeleteConstantReadingListQueue) ||
                            routingKey.Equals(DeleteUserConstantDataQueue)
                            )
                            {
                                _channel.BasicPublishAsync(exchange: string.Empty, routingKey: routingKey, mandatory: true, body: args.Body);
                                return Task.CompletedTask;
                            }


                            if (_rabbitTaskDictonary.CheakTaskLive(correlationId))
                            {
                                var prop = new BasicProperties()
                                {
                                    ReplyTo = args.BasicProperties.ReplyTo,
                                    CorrelationId = correlationId,
                                };
                                _channel.BasicPublishAsync(exchange: string.Empty, routingKey: routingKey, mandatory: true, basicProperties: prop, body: args.Body);
                            }

                        }

                    }
                    return Task.CompletedTask;
                };
                isDeclareQueues = true;
            }
        }

        public Task StartSagaConsumers()
        {
            var ResultConstantListConsumer = new AsyncEventingBasicConsumer(_channel);
            var ResultUserDataConsumer = new AsyncEventingBasicConsumer(_channel);

            ResultConstantListConsumer.ReceivedAsync += async (ch, ea) =>
            {
                await _channel.BasicAckAsync(ea.DeliveryTag, false);

                string rebitResultJson = Encoding.UTF8.GetString(ea.Body.Span);
                CreateUserSageResult rabbitResult = JsonSerializer.Deserialize<CreateUserSageResult>(rebitResultJson);
                bool result = _rabbitTaskDictonary.RealizeTask(ea.BasicProperties.CorrelationId!, rabbitResult!);
            };

             _channel.BasicConsumeAsync(
                ResultCreateConstantReadingListForUserQueue,
                false,
                ResultConstantListConsumer
                );

              ResultUserDataConsumer.ReceivedAsync += async (ch, ea) =>
              {
                  await _channel.BasicAckAsync(ea.DeliveryTag, false);

                  string rebitResultJson = Encoding.UTF8.GetString(ea.Body.Span);
                  CreateUserSageResult rabbitResult = JsonSerializer.Deserialize<CreateUserSageResult>(rebitResultJson);
                  bool result = _rabbitTaskDictonary.RealizeTask(ea.BasicProperties.CorrelationId!, rabbitResult!);
              };

              _channel.BasicConsumeAsync(
                ResultCreateUserConstantDataQueue,
                false,
                ResultUserDataConsumer
                );

            return Task.CompletedTask;
        }

        public async Task<Result> Handle(RegisterUserCred UserCreds)
        {

            var constantReadinglistTask = CreateConstantReadingList(UserCreds.UserId);
            var CreateConstantUserDateTask = CreateConstantUserDate(UserCreds);

            var res = await Task.WhenAll(constantReadinglistTask, CreateConstantUserDateTask);

            var FailsResult = res.Where(f => !f.IsSuccess).ToList();



            if (FailsResult.Count() != 0)
            {
                var SuccsesResult = res.Where(s => s.IsSuccess).ToList();
                RollBack(UserCreds.UserId, SuccsesResult);
                string errors = string.Join(Environment.NewLine, FailsResult.Select(s => s.Error));
                return Result.Failure(errors);
            }

            return Result.Success();
        }

        private async Task<CreateUserSageResult> CreateConstantReadingList(string userId)
        {
            string CorrelationId = Guid.NewGuid().ToString();
            var prop = new BasicProperties()
            {
                ReplyTo = ResultCreateConstantReadingListForUserQueue,
                CorrelationId = CorrelationId,
            };
            TaskCompletionSource<CreateUserSageResult> result = new TaskCompletionSource<CreateUserSageResult>();
            
            _rabbitTaskDictonary.SetTask(CorrelationId, result);
            var message = Encoding.UTF8.GetBytes(userId);
            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: CreateConstantReadingListQueue,mandatory:true,basicProperties: prop, body: message);
            return await result.Task;
        }

        private async Task<CreateUserSageResult> CreateConstantUserDate(RegisterUserCred usercreds)
        {
            string CorrelationId = Guid.NewGuid().ToString();
            var prop = new BasicProperties()
            {
                ReplyTo = ResultCreateUserConstantDataQueue,
                CorrelationId = CorrelationId,
            };
            TaskCompletionSource<CreateUserSageResult> result = new TaskCompletionSource<CreateUserSageResult>();
            _rabbitTaskDictonary.SetTask(CorrelationId, result);

            var message = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(usercreds));
            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: CreateUserConstantDataQueue, mandatory: true, basicProperties: prop, body: message);
            return await result.Task;
        }

        private async Task RollBack(string userId, List<CreateUserSageResult> rollbackCommand)
        {
            foreach (var command in rollbackCommand)
            {
                var message = Encoding.UTF8.GetBytes(userId);
                switch (command.Command)
                {
                    case CreateUserComandEnum.ReadingListCreationSucsess:
                        await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: DeleteUserConstantDataQueue, mandatory: true, body: message);
                        break;
                    case CreateUserComandEnum.UserDataCreationSucsess:
                        await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: DeleteConstantReadingListQueue, mandatory: true, body: message);
                        break;

                }
            }
        }

    }
}
