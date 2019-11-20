using KWKY.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System.Threading.Tasks;

namespace KWKY.WebClient.Filters
{
    ///没有之前和之后的事件。
    ///实现 OnException 或 OnExceptionAsync。
    ///处理控制器创建、模型绑定、操作筛选器或操作方法中发生的未经处理的异常。
    ///请不要捕获资源筛选器、结果筛选器或 MVC 结果执行中发生的异常。
    ///非常适合捕获发生在 MVC 操作中的异常。
    ///并不像错误处理中间件那么灵活。
    public class AsyncGlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger _logger;

        public AsyncGlobalExceptionFilter ()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }
        public async Task OnExceptionAsync (ExceptionContext context)
        {
            _logger.Error(context.Exception);
            context.Result = new JsonResult(ResponseModel.BadRequest);
            await Task.CompletedTask;
        }
    }
}
