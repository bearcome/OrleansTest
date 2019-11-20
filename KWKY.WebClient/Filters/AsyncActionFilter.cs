using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace KWKY.WebClient.Filters
{
    /// <summary>
    /// 异步Action 过滤器
    /// </summary>
    public class AsyncActionFilter : ActionFilterAttribute, IAsyncActionFilter
    {
        public override async Task OnActionExecutionAsync (ActionExecutingContext context, ActionExecutionDelegate next)
        {
#warning 暂无内容
            // do something before the action executes
            await next();
            // do something after the action executes
        }

    }
}
