
using IndentityDomain.Infrastructure.RabbitMQ.Sages;

namespace IndentityDomain.Presentation.RabbitConsumers
{
    public class CreateUserRabbitConsumer : BackgroundService
    {
        private readonly CreateUserSaga createUserSaga;

        public CreateUserRabbitConsumer(CreateUserSaga createUserSaga)
        {
            this.createUserSaga = createUserSaga;
        }

        protected override async Task  ExecuteAsync(CancellationToken stoppingToken)
        {
            await createUserSaga.StartSagaConsumers();           
        }
    }
}
