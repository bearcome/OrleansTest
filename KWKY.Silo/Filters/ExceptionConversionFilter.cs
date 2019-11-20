using NLog;
using Orleans;
using System;
using System.Threading.Tasks;

namespace KWKY.Silo.Filters
{
    /// <summary>
    /// 捕捉异常
    /// 忽略掉（客户端可能无法识别服务端的异常类型，可此处进行转换）
    /// </summary>
    public class ExceptionConversionFilter : IIncomingGrainCallFilter
    {
        private readonly ILogger _logger;
        public ExceptionConversionFilter ()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task Invoke (IIncomingGrainCallContext context)
        {

            try
            {
                await context.Invoke();
            }
            catch ( Exception exc )
            {
                _logger.Error(exc);
                var type = exc.GetType();
                throw new Exception(type.FullName);
            }
        }
    }
}
