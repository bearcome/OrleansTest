using KWKY.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace KWKY.WebClient.Filters
{
    /// <summary>
    /// //接口参数 数据模型有效性验证  
    /// </summary>
    public class AsyncModelVerfyFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync (ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if ( !context.ModelState.IsValid )
            {
                context.Result = new JsonResult(ResponseModel.ArgsInvalid);
                return;
            }
            await next();
        }
    }
}
