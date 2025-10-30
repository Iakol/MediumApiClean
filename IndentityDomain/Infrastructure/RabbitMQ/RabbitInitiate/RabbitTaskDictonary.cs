using IndentityDomain.Application.DTO;
using System.Collections.Concurrent;

namespace IndentityDomain.Infrastructure.RabbitMQ.RabbitInitiate
{
    public class RabbitTaskDictonary
    {
        private ConcurrentDictionary<string, TaskCompletionSource<CreateUserSageResult>> TaskDictonary;

        public RabbitTaskDictonary()
        {
            TaskDictonary = new ConcurrentDictionary<string, TaskCompletionSource<CreateUserSageResult>>();
        }

        public bool SetTask(string CorrelationId, TaskCompletionSource<CreateUserSageResult> createTask)
        {
            return TaskDictonary.TryAdd(CorrelationId, createTask);
        }

        public bool CheakTaskLive(string CorrelationId) 
        {
            return TaskDictonary.ContainsKey(CorrelationId);
        }


        public bool RealizeTask(string CorrelationId, CreateUserSageResult result)
        {
            TaskDictonary.TryRemove(CorrelationId, out TaskCompletionSource<CreateUserSageResult> CreateTask);

            if (CreateTask != null)
            {
                CreateTask.SetResult(result);
                return true;
            }

            return false;

        }
    }
}
