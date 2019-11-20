using NLog;
using Orleans;
using System;
using System.Threading.Tasks;

namespace KWKY.WebClient.Filters
{
    /// <summary>
    ///  调用Grains时 管道模式捕捉异常
    /// </summary>
    public class AsyncGrainExceptionFilter : IOutgoingGrainCallFilter
    {
        private readonly ILogger _logger;

        public AsyncGrainExceptionFilter ()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task Invoke (IOutgoingGrainCallContext context)
        {
            try
            {
                await context.Invoke();
            }
            catch ( Exception exception )
            {
                var msg = string.Format(
                    "\n{0}.{1}({2}) threw an exception",
                    context.Grain.GetType(),
                    context.InterfaceMethod.Name,
                    context.Arguments==null?null:string.Join(", ", context.Arguments));
                _logger.Error(exception, msg);
                throw;
            }
        }
    }
}
