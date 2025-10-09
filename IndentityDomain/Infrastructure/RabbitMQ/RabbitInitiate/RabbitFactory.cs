using IndentityDomain.Infrastructure.RabbitMQ;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace IndentityDomain.Infrastructure.RabbitMQ.RabbitInitiate
{
    public class RabbitFactory
    {
        private readonly IOptions<RabbitLogCreds> _RabbitOpt;
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IChannel _channel;

        public RabbitFactory(IOptions<RabbitLogCreds> options)
        {
            _RabbitOpt = options;
            _connectionFactory = new ConnectionFactory
            {
                HostName = _RabbitOpt.Value.HostName,
                UserName = _RabbitOpt.Value.UserName,
                Password = _RabbitOpt.Value.Password,
                Port = _RabbitOpt.Value.Port,
            };

            _connection = _connectionFactory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;

        }
        public IChannel GetConnectionChanel()
        {

            return _channel;
        }

        public Dictionary<string, object?> CreateDeadLeaterArgumentsForQueue(string DeadQueue, int ttlTime) 
        {
            return new Dictionary<string, object?>()
            {
                {"x-message-ttl",ttlTime },
                {"x-dead-letter-exchange","" },
                {"x-dead-letter-routing-key",DeadQueue },
            };

        }

    }
}
