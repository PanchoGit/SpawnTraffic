using System.Collections.Generic;
using System.Linq;

namespace SpawnTraffic.Common.Domains
{
    public class Result
    {
        public List<ResultMessage> Messages { get; set; }

        public Result()
        {
            Messages = new List<ResultMessage>();
        }

        public virtual bool HasErrors => Messages.Any(s => s.type == ResultMessageType.Error);

        public void AddSuccess(string message)
        {
            Messages.Add(new ResultMessage
            {
                type = ResultMessageType.Success,

                Message = message
            });
        }

        public void AddError(string message)
        {
            Messages.Add(new ResultMessage
            {
                type = ResultMessageType.Error,

                Message = message
            });
        }

        public void AddMessages(List<ResultMessage> messages)
        {
            Messages.AddRange(messages);
        }

        public void AddMessages(Result result)
        {
            Messages.AddRange(result.Messages);
        }
    }

    public sealed class Result<T> : Result
    {
        public Result()
        {
            
        }

        public Result(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}